using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Repos
{
	internal class MakeRepo : IMakeRepo
	{
		public int Add(Make entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int AddRange(IEnumerable<Make> entities, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int Delete(int id, byte[] timeStamp, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int Delete(Make entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int DeleteRange(IEnumerable<Make> entities, bool persist = true)
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

		public Make? Find(int? id)
		{
			throw new NotImplementedException();
		}

		public Make? FindAsNoTracking(int id)
		{
			throw new NotImplementedException();
		}

		public Make? FindIgnoreQueryFilters(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Make> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Make> GetAllIgnoreQueryFilters()
		{
			throw new NotImplementedException();
		}

		public int SaveChanges()
		{
			throw new NotImplementedException();
		}

		public int Update(Make entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int UpdateRange(IEnumerable<Make> entities, bool persist = true)
		{
			throw new NotImplementedException();
		}
	}
}
