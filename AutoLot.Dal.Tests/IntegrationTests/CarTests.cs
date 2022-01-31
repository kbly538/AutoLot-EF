using AutoLot.Dal.Tests.Base;
using AutoLot.Models.Entities;
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
	public class CarTests : BaseTest, IClassFixture<EnsureAutoLotDatabasetTestFixture>
	{

		[Theory]
		[InlineData(1, 2)]
		[InlineData(2, 1)]
		[InlineData(3, 1)]
		[InlineData(4, 2)]
		[InlineData(5, 3)]
		[InlineData(6, 1)]
		public void ShouldGetTheCarsByMake(int makeId, int expectedCount)
		{
			IQueryable<Car> query = Context.Cars
				.IgnoreQueryFilters().Where(c => c.MakeId == makeId);
			var qs = query.ToQueryString();
			var cars = query.ToList();
			Assert.Equal(expectedCount, cars.Count);
		}
	}
}
	