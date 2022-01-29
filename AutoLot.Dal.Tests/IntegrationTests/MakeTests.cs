
using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Dal.Tests.Base;
using AutoLot.Dal.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
	}
}
