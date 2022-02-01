using AutoLot.Dal.EfStructures;
using AutoLot.Dal.Repos.Base;
using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Repos
{
	public class CarRepo : BaseRepo<Car>, ICarRepo
	{
		public CarRepo(ApplicationDbContext context) : base(context){}

		public CarRepo(DbContextOptions<ApplicationDbContext> options) : base(options){}

		public IEnumerable<Car> GetAllBy(int makeId)
		{
			return Table.Where(c => c.Id == makeId).Include(c => c.MakeNavigation).OrderBy(o => o.PetName);
		}

		public string GetPetName(int id)
		{
			var parameterId = new SqlParameter
			{
				ParameterName = "@carId",
				SqlDbType = SqlDbType.Int,
				Value = id
			};

			var parameterName = new SqlParameter
			{
				ParameterName = "@petName",
				SqlDbType = SqlDbType.NVarChar,
				Size = 50,
				Direction = ParameterDirection.Output

			};

			var result = Context.Database.ExecuteSqlRaw(
				"EXEC [dbo].[GetPetName] @carId, @petName OUTPUT", parameterId, parameterName);			
			return (string) parameterName.Value;

		}

		public override Car? Find(int? id)
		{
			return Table.IgnoreQueryFilters().Where(c => c.Id == id).Include(m => m.MakeNavigation).FirstOrDefault();
		}

		public override IEnumerable<Car> GetAll()
		{
			return Table.Include(c => c.MakeNavigation).OrderBy(o => o.PetName);
		}

		public override IEnumerable<Car> GetAllIgnoreQueryFilters()
		{
			return Table.Include(c => c.MakeNavigation).OrderBy(o => o.PetName).IgnoreQueryFilters();
		}

	}
}
