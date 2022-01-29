using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLot.Dal;
using AutoLot.Dal.Initialization;

namespace AutoLot.Dal.Tests.Base
{
	public sealed class EnsureAutoLotDatabasetTestFixture : IDisposable
	{
		public EnsureAutoLotDatabasetTestFixture()
		{
			var configuration = TestHelpers.GetConfiguration();
			var context = TestHelpers.GetContext(configuration);
			SampleData.ClearAndReseedDatabase(context);
			context.Dispose();
		}

		public void Dispose(){}
	}
}
