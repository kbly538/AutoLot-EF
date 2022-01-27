using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutoLot.Samples
{
	public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			string connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog= AutoLot;Integrated Security=True;Connect Timeout=5";
			optionsBuilder.UseSqlServer(connectionString);
			Console.WriteLine(connectionString);
			return new ApplicationDbContext(optionsBuilder.Options);
		}

		static void SampleSaveChanges()
		{
			var context = new ApplicationDbContextFactory().CreateDbContext(null);
			var strategy = context.Database.CreateExecutionStrategy();
			strategy.Execute(() =>
			{
				using var trans = context.Database.BeginTransaction();
				try
				{
					trans.CreateSavepoint("check point 1");
					context.SaveChanges();
					trans.Commit();
				}
				catch (Exception ex)
				{
					trans.RollbackToSavepoint("check point 1");
				}
			}
			);


			
		}
	}
}
