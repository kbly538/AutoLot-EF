using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Repos
{
	internal class CreditRiskRepo : ICreditRiskRepo
	{
		public int Add(CreditRisk entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int AddRange(IEnumerable<CreditRisk> entities, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int Delete(int id, byte[] timeStamp, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int Delete(CreditRisk entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int DeleteRange(IEnumerable<CreditRisk> entities, bool persist = true)
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

		public CreditRisk? Find(int? id)
		{
			throw new NotImplementedException();
		}

		public CreditRisk? FindAsNoTracking(int id)
		{
			throw new NotImplementedException();
		}

		public CreditRisk? FindIgnoreQueryFilters(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CreditRisk> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CreditRisk> GetAllIgnoreQueryFilters()
		{
			throw new NotImplementedException();
		}

		public int SaveChanges()
		{
			throw new NotImplementedException();
		}

		public int Update(CreditRisk entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int UpdateRange(IEnumerable<CreditRisk> entities, bool persist = true)
		{
			throw new NotImplementedException();
		}
	}
}
