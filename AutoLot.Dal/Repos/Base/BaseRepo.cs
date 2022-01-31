using AutoLot.Dal.EfStructures;
using AutoLot.Dal.Exceptions;
using AutoLot.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Repos.Base
{
	public abstract class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
	{

		private readonly bool _disposeContext;
		public ApplicationDbContext Context { get; }
		public DbSet<T> Table { get; }

		protected BaseRepo(ApplicationDbContext context)
		{
			Context = context;
			Table = Context.Set<T>();
			_disposeContext = false;
		}


		protected BaseRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options))
		{
			_disposeContext = true;
		}

		public virtual void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private bool _isDisposed;
		public void Dispose(bool dispoing)
		{
			if (_isDisposed)
			{
				return;
			}
			if (dispoing)
			{
				if (_disposeContext)
				{
					Context.Dispose();
				}
			}
			_isDisposed = true;	
		}

		~BaseRepo()
		{
			Dispose(false);
		}

		public virtual int Add(T entity, bool persist = true)
		{
			Table.Add(entity);
			return persist ? SaveChanges() : 0;
		}

		public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
		{
			Table.AddRange(entities);
			return persist ? SaveChanges() : 0;
		}

		public virtual int Delete(int id, byte[] timeStamp, bool persist = true)
		{
			var entity = new T { Id = id , TimeStamp = timeStamp};
			Context.Entry(entity).State = EntityState.Deleted;
			return persist ? SaveChanges() : 0;
			
		}

		public virtual int Delete(T entity, bool persist = true)
		{
			Table.Remove(entity);
			return persist ? SaveChanges() : 0;
		}

		public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
		{
			Table.RemoveRange(entities);
			return persist ? SaveChanges() : 0;
		}

		public void ExecuteQuery(string sql, object[] sqlParametersObjects)
			=> ExecuteQuery(sql, sqlParametersObjects);

		public virtual T? Find(int? id) 
			=> Table.Find(id);

		public virtual T? FindAsNoTracking(int id)
			=> Table.AsNoTrackingWithIdentityResolution().FirstOrDefault(e => e.Id == id);
		
		public virtual T? FindIgnoreQueryFilters(int id)
			=> Table.IgnoreQueryFilters().FirstOrDefault(e => e.Id == id);


		public virtual IEnumerable<T> GetAll() 
			=> Table;

		public virtual IEnumerable<T> GetAllIgnoreQueryFilters() 
			=> Table.IgnoreQueryFilters();

		public int SaveChanges()
		{
			try
			{
				return Context.SaveChanges();
			}
			catch (CustomException ex)
			{
					throw;
			}
			catch (Exception ex)
			{
				throw new CustomException("An error occured updating the database.", ex);
			}
		}

		public int Update(T entity, bool persist = true)
		{
			Table.Update(entity);
			return persist ? SaveChanges() : 0;
		}

		public int UpdateRange(IEnumerable<T> entities, bool persist = true)
		{
			Table.UpdateRange(entities);
			return persist ? SaveChanges() : 0;
		}
	}
}
