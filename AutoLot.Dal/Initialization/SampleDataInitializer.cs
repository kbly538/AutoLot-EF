using AutoLot.Dal.EfStructures;
using AutoLot.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Initialization
{
	public static class SampleDataInitializer
	{
		public static void DropAndCreateDatabase(ApplicationDbContext context)
		{
			context.Database.EnsureDeleted();
			context.Database.Migrate();
		}
		
		internal static void ClearData(ApplicationDbContext context)
		{
			var entities = new[]
			{
				typeof(Order).FullName,
				typeof(Customer).FullName,
				typeof(Car).FullName,
				typeof(Make).FullName,
				typeof(CreditRisk).FullName

			};

			foreach (var entityName in entities)
			{
				var entity = context.Model.FindEntityType(entityName);
				var tableName = entity.GetTableName();
				var schemaName = entity.GetSchema();
				context.Database.ExecuteSqlRaw($"DELETE FROM {schemaName}.{tableName}");
				context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT (\"{schemaName}.{tableName}\", RESEED, 1);");
			} 

		}
	}

	
}
