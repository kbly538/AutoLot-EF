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

		[Fact]
		public void ShouldGetReferenceRelatedInformationExplicitly()
		{
			var car = Context.Cars.First(x => x.Id == 1);
			Assert.Null(car.MakeNavigation);
			var query = Context.Entry(car).Reference(c => c.MakeNavigation).Query();
			var qs = query.ToQueryString();
			query.Load();
			Assert.NotNull(car.MakeNavigation);
		}

		[Fact]
		public void ShouldGetCollectionRelatedInformationExplicitly()
		{
			var car = Context.Cars.First(c => c.Id == 1);
			var query = Context.Entry(car).Collection(c => c.Orders).Query();
			var qs = query.ToQueryString();
			query.Load();
			Assert.Single(car.Orders);
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(2, 1)]
		[InlineData(3, 1)]
		[InlineData(4, 2)]
		[InlineData(5, 3)]
		[InlineData(6, 1)]
		public void ShouldGetAllCarsExplicitlyForAMakeWithQueryFilters(int makeId, int carCount)
		{

			var make = Context.Makes.First(x => x.Id == makeId);
			IQueryable<Car> query = Context.Entry(make).Collection(c => c.Cars).Query();
			var qs = query.ToQueryString();
			query.Load();
			Assert.Equal(carCount, make.Cars.Count());
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(2, 1)]
		[InlineData(3, 1)]
		[InlineData(4, 2)]
		[InlineData(5, 3)]
		[InlineData(6, 1)]
		public void ShouldGetAllCarsExplicitlyForAMakeWithoutQueryFilters(int makeId, int carCount)
		{

			var make = Context.Makes.First(x => x.Id == makeId);
			IQueryable<Car> query = Context.Entry(make).Collection(c => c.Cars).Query().IgnoreQueryFilters();
			var qs = query.ToQueryString();
			query.Load();
			Assert.Equal(carCount, make.Cars.Count());
		}

		[Fact]
		public void ShouldNotGetTheLemondsFromUsingSql()
		{
			var entity = Context.Model.FindEntityType($"{typeof(Car).FullName}");
			var table = entity.GetTableName();
			var schema = entity.GetSchema();
			var cars = Context.Cars.FromSqlRaw($"SELECT * FROM {schema}.{table}").ToList();
			Assert.Equal(9, cars.Count);
		}

		[Fact]
		public void ShouldGetTheLemondsFromUsingSqlWithIgnoreQueryFilters()
		{
			var entity = Context.Model.FindEntityType($"{typeof(Car).FullName}");
			var table = entity.GetTableName();
			var schema = entity.GetSchema();
			var cars = Context.Cars.FromSqlRaw($"SELECT * FROM {schema}.{table}")
				.IgnoreQueryFilters().ToList();
			Assert.Equal(10, cars.Count);
		}

		[Fact]
		public void ShouldGetOneCarUsingInterpolation()
		{
			int carId = 1;
			var car = Context.Cars.FromSqlInterpolated($"SELECT * FROM dbo.Inventory WHERE Id == {carId}")
				.Include(x => x.MakeNavigation)
				.First();
			Assert.Equal("Black", car.Color);
			Assert.Equal("VW", car.MakeNavigation.Name);
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(2, 1)]
		[InlineData(3, 1)]
		[InlineData(4, 2)]
		[InlineData(5, 3)]
		[InlineData(6, 1)]
		public void ShouldGetAllCarsByMakingUseOfFromSql(int makeId, int expectedCount)
		{

			var entity = Context.Model.FindEntityType($"{typeof(Car).FullName}");
			var tableName = entity.GetTableName();
			var schema = entity.GetSchema();
			var cars = Context.Cars.FromSqlRaw($"SELECT * FROM {schema}.{tableName}")
				.Where(x => x.MakeId == makeId).ToList();
			Assert.Equal(expectedCount, cars.Count);
		}

	}


}
	