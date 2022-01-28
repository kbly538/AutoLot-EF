using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Models.Entities;
using AutoLot.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Repos
{
	internal class OrderRepo : IOrderRepo
	{
		public int Add(Order entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int AddRange(IEnumerable<Order> entities, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int Delete(int id, byte[] timeStamp, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int Delete(Order entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int DeleteRange(IEnumerable<Order> entities, bool persist = true)
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

		public Order? Find(int? id)
		{
			throw new NotImplementedException();
		}

		public Order? FindAsNoTracking(int id)
		{
			throw new NotImplementedException();
		}

		public Order? FindIgnoreQueryFilters(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Order> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Order> GetAllIgnoreQueryFilters()
		{
			throw new NotImplementedException();
		}

		public IQueryable<CustomerOrderViewModel> GetOrdersViewModel()
		{
			throw new NotImplementedException();
		}

		public int SaveChanges()
		{
			throw new NotImplementedException();
		}

		public int Update(Order entity, bool persist = true)
		{
			throw new NotImplementedException();
		}

		public int UpdateRange(IEnumerable<Order> entities, bool persist = true)
		{
			throw new NotImplementedException();
		}
	}
}
