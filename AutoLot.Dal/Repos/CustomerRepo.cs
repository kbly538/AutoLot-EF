using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Repos
{
	internal class CustomerRepo : ICustomerRepo
	{
		public int Add(Customer entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int AddRange(IEnumerable<Customer> entities, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int Delete(int id, byte[] timeStamp, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int Delete(Customer entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int DeleteRange(IEnumerable<Customer> entities, bool persist = true)
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

		public Customer? Find(int? id)
		{
			throw new NotImplementedException();
		}

		public Customer? FindAsNoTracking(int id)
		{
			throw new NotImplementedException();
		}

		public Customer? FindIgnoreQueryFilters(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Customer> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Customer> GetAllIgnoreQueryFilters()
		{
			throw new NotImplementedException();
		}

		public int SaveChanges()
		{
			throw new NotImplementedException();
		}

		public int Update(Customer entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int UpdateRange(IEnumerable<Customer> entities, bool persist = true)
		{
			throw new NotImplementedException();
		}
	}
}
