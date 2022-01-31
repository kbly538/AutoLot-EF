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
	public class CustomerTests : BaseTest, IClassFixture<EnsureAutoLotDatabasetTestFixture>
	{
		[Fact]
		public void SHouldGetAllOfTheCustomers()
		{
			var qs = Context.Customers.ToQueryString();
			var customers = Context.Customers.ToList();
			Assert.Equal(5, customers.Count);
		}

		[Fact]
		public void ShouldGetCustomerWithLastNameW()
		{
			IQueryable<Customer> query = Context.Customers.Where(x => x.PersonalInformation.LastName.StartsWith("W"));
			var qs = query.ToQueryString();
			List<Customer> customers = query.ToList();
			Assert.Equal(2, customers.Count);
		}

		[Fact]
		public void ShouldGetCustomersWithLastNameWFirstNameM()
		{
			IQueryable<Customer> query = Context.Customers.Where(x => x.PersonalInformation.LastName.StartsWith("W"))
				.Where(x => x.PersonalInformation.FirstName.StartsWith("M"));
			var qs = query.ToQueryString();
			List<Customer> customers = query.ToList();
			Assert.Single(customers);

		}

		[Fact]
		public void ShouldGetCustomersWithLastNameWAndFirstNameM()
		{
			IQueryable<Customer> query = Context.Customers
				.Where(x => x.PersonalInformation.LastName.StartsWith("W") && x.PersonalInformation.FirstName.StartsWith("M"));
			var qs = query.ToQueryString();
			List<Customer> customers = query.ToList();
			Assert.Single(customers);
		}

		[Fact]
		public void ShouldGetCustomersWithLastNameWOrH()
		{
			IQueryable<Customer> query = Context.Customers
				.Where(x => x.PersonalInformation.LastName.StartsWith("W") || x.PersonalInformation.LastName.StartsWith("H"));
			var qs = query.ToQueryString();
			List<Customer> customers = query.ToList();
			Assert.Equal(3, customers.Count);
		}

		[Fact]
		public void EfShouldGetCustomersWithLastNameWOrH()
		{
			IQueryable<Customer> query = Context.Customers
				.Where(x => EF.Functions.Like(x.PersonalInformation.LastName, "W%") || EF.Functions.Like(x.PersonalInformation.LastName, "H%"));
			var qs = query.ToQueryString();
			List<Customer> customers = query.ToList();
			Assert.Equal(3, customers.Count);

		}







	}
}
