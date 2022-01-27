using AutoLot.Samples.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Samples
{
	public partial class ApplicationDbContext : DbContext
	{
		
		
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			SavingChanges += (sender, args) =>
			{
				Console.WriteLine($"Saving changes for {((DbContext)sender).Database.GetConnectionString()}");

			};
			SavedChanges += (sender, args) =>
			{
				Console.WriteLine($"Saved {args.EntitiesSavedCount} entities");
			};
			SaveChangesFailed += (sender, args) =>
			{
				Console.WriteLine($"An exception occurred! {args.Exception.Message} entities");

			};


		}

		public DbSet<Car>? Cars { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<BaseEntity>().ToTable("BaseEntities");
			//modelBuilder.Entity<Car>().ToTable("Cars");
			OnModelCreatingPartial(modelBuilder);
		}
		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


	
		
	}

}
