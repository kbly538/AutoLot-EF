using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace AutoLot.Dal.EfStructures
{
	public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AutoLot;Integrated Security=True;Connect Timeout=30;";
			optionsBuilder.UseSqlServer(connectionString);
			Console.WriteLine(connectionString);
			return new ApplicationDbContext(optionsBuilder.Options);
		}
	}
}
