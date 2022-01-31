using AutoLot.Dal.Tests.Base;
using AutoLot.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

		[Fact]
		public void ShouldReturnDrivableCarsWithQueryFilterSet()
		{
			IQueryable<Car> query = Context.Cars;
			var qs = query.ToQueryString();
			var cars = query.ToList();
			Assert.NotEmpty(cars);
			Assert.Equal(9, cars.Count);
		}

		[Fact]
		public void ShouldGetAllOfTheCars()
		{
			IQueryable<Car> query = Context.Cars.IgnoreQueryFilters();
			var qs = query.ToQueryString();
			var cars = query.ToList();
			Assert.Equal(10, cars.Count);
		}

		[Fact]
		public void ShouldGetAllOfTheCarsWithMakes()
		{
			IIncludableQueryable<Car, Make?> query = Context.Cars.Include(c => c.MakeNavigation);
			var queryString = query.ToQueryString();
			var cars = query.ToList();
			Assert.Equal(9, cars.Count);
		}

		[Fact]
		public void ShouldGetCarsOnOrderWithRelatedProperties()
		{
			IQueryable<Car> query = Context.Cars
				.Where(c => c.Orders.Any())
				.Include(c => c.MakeNavigation)
				.Include(c => c.Orders).ThenInclude(o => o.CustomerNavigation)
				.AsSplitQuery();
			var queryString = query.ToQueryString();
			var cars = query.ToList();
			Assert.Equal(4, cars.Count);
			cars.ForEach(c =>
			{
				Assert.NotNull(c.MakeNavigation);
				Assert.NotNull(c.Orders.ToList()[0].CustomerNavigation);
			});
		}

	}


}
	