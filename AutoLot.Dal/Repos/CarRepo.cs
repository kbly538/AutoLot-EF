using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Repos
{
	internal class CarRepo : ICarRepo
	{
		public int Add(Car entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int AddRange(IEnumerable<Car> entities, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int Delete(int id, byte[] timeStamp, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int Delete(Car entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int DeleteRange(IEnumerable<Car> entities, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public void ExecuteQuery(string sql, object[] sqlParametersObjects)
		{
			throw new NotImplementedException();
		}

		public Car? Find(int? id)
		{
			throw new NotImplementedException();
		}

		public Car? FindAsNoTracking(int id)
		{
			throw new NotImplementedException();
		}

		public Car? FindIgnoreQueryFilters(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Car> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Car> GetAllBy(int makeId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Car> GetAllIgnoreQueryFilters()
		{
			throw new NotImplementedException();
		}

		public string GetPetName(int id)
		{
			throw new NotImplementedException();
		}

		public int SaveChanges()
		{
			throw new NotImplementedException();
		}

		public int Update(Car entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int UpdateRange(IEnumerable<Car> entities, bool persist = true)
		{
			throw new NotImplementedException();
		}
	}
}
