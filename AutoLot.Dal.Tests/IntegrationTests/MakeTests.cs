
using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Dal.Tests.Base;
using AutoLot.Dal.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace AutoLot.Dal.Tests.IntegrationTests
{
	[Collection("Integration Tests")]
	public class MakeTests : BaseTest, IClassFixture<EnsureAutoLotDatabasetTestFixture>
	{
		private readonly IMakeRepo _repo;
		public MakeTests()
		{
			_repo = new MakeRepo(Context);
		}
		
		public override void Dispose()
		{
			_repo.Dispose();
		}

		[Fact]
		public void ShouldGetAllMakesAndCarsThatAreYellow()
		{
			var query = Context.Makes.IgnoreQueryFilters().Include(x => x.Cars.Where(x => x.Color == "Yellow"));
			var qs = query.ToQueryString();
			var makes = query.ToList();
			Assert.NotNull(makes);
			Assert.NotEmpty(makes);
			Assert.NotEmpty(makes.Where(x => x.Cars.Any()));
			Assert.Empty(makes.First(m => m.Id == 1).Cars);
			Assert.Empty(makes.First(m => m.Id == 2).Cars);
			Assert.Empty(makes.First(m => m.Id == 3).Cars);
			Assert.Single(makes.First(m => m.Id == 4).Cars);
			Assert.Empty(makes.First(m => m.Id == 5).Cars);


		}
	}
}
