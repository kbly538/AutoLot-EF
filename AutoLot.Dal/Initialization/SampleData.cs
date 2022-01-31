using AutoLot.Dal.EfStructures;
using AutoLot.Models.Entities;
using AutoLot.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Initialization
{
	public static class SampleData
	{
		public static List<Customer> Customers => new() 
		{   
			new() { Id = 1, PersonalInformation = new() { FirstName = "Dave", LastName = "Brenner" } }, 
		    new() { Id = 2, PersonalInformation = new() { FirstName = "Matt", LastName = "Walton" } }, 
			new() { Id = 3, PersonalInformation = new() { FirstName = "Steve", LastName = "Hagen" } }, 
			new() { Id = 4, PersonalInformation = new() { FirstName = "Pat", LastName = "Walton" } }, 
			new() { Id = 5, PersonalInformation = new() { FirstName = "Bad", LastName = "Customer" } }
		};

		public static List<Make> Makes => new() 
		{ 
			new() { Id = 1, Name = "VW" }, 
			new() { Id = 2, Name = "Ford" }, 
			new() { Id = 3, Name = "Saab" }, 
			new() { Id = 4, Name = "Yugo" }, 
			new() { Id = 5, Name = "BMW" }, 
			new() { Id = 6, Name = "Pinto" }, 
		};

		public static List<Car> Inventory => new()
		{
			new() { Id = 1, MakeId = 1, Color = "Black", PetName = "Zippy" },
			new() { Id = 2, MakeId = 2, Color = "Rust", PetName = "Rusty" },
			new() { Id = 3, MakeId = 3, Color = "Black", PetName = "Mel" },
			new() { Id = 4, MakeId = 4, Color = "Yellow", PetName = "Clunker" },
			new() { Id = 5, MakeId = 5, Color = "Black", PetName = "Bimmer" },
			new() { Id = 6, MakeId = 5, Color = "Green", PetName = "Hank" },
			new() { Id = 7, MakeId = 5, Color = "Pink", PetName = "Pinky" },
			new() { Id = 8, MakeId = 6, Color = "Black", PetName = "Pete" },
			new() { Id = 9, MakeId = 4, Color = "Brown", PetName = "Brownie" },
			new() { Id = 10, MakeId = 1, Color = "Rust", PetName = "Lemon", IsDrivable = false }
		};

		public static List<Order> Orders => new() 
		{ 
			new() { Id = 1, CustomerId = 1, CarId = 5 }, 
			new() { Id = 2, CustomerId = 2, CarId = 1 }, 
			new() { Id = 3, CustomerId = 3, CarId = 4 }, 
			new() { Id = 4, CustomerId = 4, CarId = 7 }, 
			new() { Id = 5, CustomerId = 5, CarId = 10 }
		};

		public static List<CreditRisk> CreditRisks => new() 
			{ 
			new() 
			{ 
				Id = 1, 
				CustomerId = Customers[4].Id, 
				PersonalInformation = new() 
				{ 
					FirstName = Customers[4].PersonalInformation.FirstName, 
					LastName = Customers[4].PersonalInformation.LastName 
				} 
			} 
		};

		internal static void SeedData(ApplicationDbContext context)
		{
			try
			{
				ProcessInsert(context, context.Customers!, SampleData.Customers);
				ProcessInsert(context, context.Makes!, SampleData.Makes);
				ProcessInsert(context, context.Cars!, SampleData.Inventory);
				ProcessInsert(context, context.Orders!, SampleData.Orders);
				ProcessInsert(context, context.CreditRisks!, SampleData.CreditRisks);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
		}

		private static void ProcessInsert<TEntity>(
			ApplicationDbContext context, 
			DbSet<TEntity> table, 
			List<TEntity> records) where TEntity : BaseEntity
		{
			if (table.Any())
			{
				return;
			}
			IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
			strategy.Execute(() =>
		    {
			   using var transaction = context.Database.BeginTransaction();
			   try
			   {
				   var metaData = context.Model.FindEntityType(typeof(TEntity).FullName);
				   context.Database.ExecuteSqlRaw(
					   $"SET IDENTITY_INSERT {metaData.GetSchema()}.{metaData.GetTableName()} ON");
				   table.AddRange(records);
				   context.SaveChanges();
				   context.Database.ExecuteSqlRaw(
					   $"SET IDENTITY_INSERT {metaData.GetSchema()}.{metaData.GetTableName()} OFF");
				   transaction.Commit();
			   } catch (Exception ex)
			   {
				   transaction.Rollback();
			   }
			});


		}

		public static void InitializeData(ApplicationDbContext context)
		{
			SampleDataInitializer.DropAndCreateDatabase(context);
			SeedData(context);
		}

		public static void ClearAndReseedDatabase(ApplicationDbContext context)
		{
			SampleDataInitializer.ClearData(context);
			SeedData(context);
		}
	}
}
