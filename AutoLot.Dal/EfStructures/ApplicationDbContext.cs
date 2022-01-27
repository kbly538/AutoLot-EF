using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoLot.Models.Entities;
using AutoLot.Models.Entities.Owned;
using Microsoft.EntityFrameworkCore.Storage;
using AutoLot.Dal.Exceptions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AutoLot.Dal.EfStructures
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            base.SavingChanges += (sender, args) =>
			{
				Console.WriteLine($"Saving to {((ApplicationDbContext)sender)!.Database!.GetConnectionString()}");


			};

            base.SavedChanges += (sender, args) =>
            {
                Console.WriteLine($"Saved {args!.EntitiesSavedCount} changes to {((ApplicationDbContext)sender)!.Database!.GetConnectionString()}");
            };

            base.SaveChangesFailed += (sender, args) => 
            {
                Console.WriteLine($"An exception occured {args.Exception.Message} entites.");
            };

            ChangeTracker.Tracked += ChangeTracker_Tracked;
            ChangeTracker.StateChanged += ChangeTracker_StateChanged;


        }

		private void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs e)
		{
            if (e.Entry.Entity is not Car c)
			{
                return;
			}
            var action = string.Empty;
            Console.WriteLine($"Car {c.PetName} was {e.OldState} before state changed to {e.NewState}");
            switch (e.NewState)
			{
                case EntityState.Unchanged:
                    action = e.OldState switch
                    {
                        EntityState.Added => "Added",
                        EntityState.Modified => "Edited",
                        _ => action
                    };
                    Console.WriteLine($"The object was {action}");
                    break;
                 
			}
		}

		private void ChangeTracker_Tracked(object? sender, EntityTrackedEventArgs e)
		{
            var source = (e.FromQuery) ? "Database" : "Code";
            if (e.Entry.Entity is Car c)
			{
                Console.WriteLine($"Car entity {c.PetName} was added from {source}");
			}
        }

		public DbSet<SeriLogEntry>? SeriLogs { get; set; }
        public DbSet<CreditRisk>? CreditRisks { get; set; }
        public DbSet<Customer>? Customers { get; set; } 
        public DbSet<Car>? Cars { get; set; }
        public DbSet<Make>? Makes { get; set; }
        public DbSet<Order>? Orders { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeriLogEntry>(entity =>
            {
                entity.Property(e => e.Properties).HasColumnType("Xml");
                entity.Property(e => e.TimeStamp).HasDefaultValueSql("GetDate()");
            });


            modelBuilder.Entity<CreditRisk>(entity =>
            {

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p!.CreditRisks)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CreditRisks_Customers");



                entity.OwnsOne(o => o.PersonalInformation,
                    pd =>
                    {
                        pd.Property<string>(nameof(Person.FirstName))
                            .HasColumnName(nameof(Person.FirstName))
                            .HasColumnType("nvarchar(50)");
                        pd.Property<string>(nameof(Person.FirstName))
                            .HasColumnName(nameof(Person.FirstName))
                            .HasColumnType("nvarchar(50)");
                        pd.Property<string>(nameof(Person.FullName))
                            .HasColumnName(nameof(Person.FullName))
                            .HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
                    });

//                entity.HasIndex(e => e.CustomerId, "IX_CreditRisks_CustomerId");

            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.OwnsOne(o => o.PersonalInformation,
                    pd =>
                    {
                        pd.Property(p => p.FirstName).HasColumnName(nameof(Person.FirstName));
                        pd.Property(p => p.LastName).HasColumnName(nameof(Person.LastName));
                        pd.Property(p => p.FullName).HasColumnName(nameof(Person.FullName))
                            .HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
                    });
            });

            modelBuilder.Entity<Car>(entity =>
            {

                entity.HasQueryFilter(c => c.IsDrivable);
                entity.Property(p => p.IsDrivable).HasField("_isDrivable").HasDefaultValue(true);

                entity.HasOne(d => d.MakeNavigation)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.MakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Make_Inventory");
            });

            modelBuilder.Entity<Make>(entity =>
            {
                entity.HasMany(e => e.Cars)
                    .WithOne(c => c.MakeNavigation!)
                    .HasForeignKey(k => k.MakeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Make_Inventory");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.CarNavigation)
                    .WithMany(p => p!.Orders)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Inventory");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p!.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");
            });

            modelBuilder.Entity<Order>().HasQueryFilter(e => e.CarNavigation!.IsDrivable);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

		public override int SaveChanges()
		{
			try
			{
                return base.SaveChanges();

			}
            catch (DbUpdateConcurrencyException ex)
			{
                // log and handle internally
                throw new CustomConcurrencyException("A concurrency error occured.", ex);
			}
            catch (RetryLimitExceededException ex)
			{
                throw new CustomRetryLimitExceededException("There is a problem with SQL server.", ex);
			}
            catch (DbUpdateException ex) 
            {
                throw new CustomDbUpdateException("An error occured updating the database.", ex);
            } 
            catch (Exception ex)
			{
                throw new CustomException("An error occured updating the database.", ex);
			}
		}
	}
}
