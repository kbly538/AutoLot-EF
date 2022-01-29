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
	public class CustomerTests : BaseTest, IClassFixture<EnsureAutoLotDatabasetTestFixture>
	{
	}
}
