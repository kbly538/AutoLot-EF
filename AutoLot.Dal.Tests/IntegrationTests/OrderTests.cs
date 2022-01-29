using AutoLot.Dal.Repos;
using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Dal.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutoLot.Dal.Tests.IntegrationTests
{
	[Collection("Integration Tests")]
	public class OrderTests : BaseTest, IClassFixture<EnsureAutoLotDatabasetTestFixture>
	{
		private readonly IOrderRepo _repo;

		public OrderTests()
		{
			_repo = new OrderRepo(Context);
		}

		public override void Dispose()
		{
			_repo.Dispose();
		}
	}
}
