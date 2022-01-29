using AutoLot.Dal.EfStructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Tests.Base
{
	public abstract class BaseTest : IDisposable
	{
		protected readonly IConfiguration Configuration;
		protected readonly ApplicationDbContext Context;

		public BaseTest()
		{
			Configuration = TestHelpers.GetConfiguration();
			Context = TestHelpers.GetContext(Configuration);
		}
		
		public void Dispose()
		{
			Context.Dispose();
		}

		public void ExecuteInATransaction(Action actionToExecute)
		{
			var strategy = Context.Database.CreateExecutionStrategy();
			strategy.Execute(() =>
			{
				using var trans = Context.Database.BeginTransaction();
				actionToExecute();
				trans.Rollback();
			});
		}

		public void ExecuteInSharedTransaction(Action<IDbContextTransaction> actionToExecute)
		{
			var strategy = Context.Database.CreateExecutionStrategy();
			strategy.Execute(() =>
			{
				using IDbContextTransaction trans =
				Context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
				actionToExecute(trans);
				trans.Rollback();
			});
		}


		
	}
}
