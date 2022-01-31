using AutoLot.Dal.Repos;
using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Dal.Tests.Base;
using Microsoft.EntityFrameworkCore;
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

		[Fact]
		public void ShouldGetAllViewModels()
		{
			var qs = Context.Orders.ToQueryString();
			var orders = Context.Orders.ToList();
			Assert.NotEmpty(orders);
			Assert.Equal(4, orders.Count);
			
		}

		[Theory]
		[InlineData("Black", 2)]
		[InlineData("Rust", 1)]
		[InlineData("Yellow", 1)]
		[InlineData("Green", 0)]
		[InlineData("Pink", 1)]
		[InlineData("Brown", 0)]
		public void ShouldGetAllViewModelsByColor(string color, int expectedCount)
		{
			var query = _repo.GetOrdersViewModel().Where(o => o.Color == color);
			var qs = query.ToQueryString();
			var orders = query.ToList();
			Assert.Equal(expectedCount, orders.Count);
		}
	}
}
