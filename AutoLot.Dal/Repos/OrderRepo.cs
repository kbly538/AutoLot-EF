using AutoLot.Dal.EfStructures;
using AutoLot.Dal.Repos.Base;
using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Models.Entities;
using AutoLot.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Repos
{
	internal class OrderRepo : BaseRepo<Order>, IOrderRepo
	{
		public OrderRepo(ApplicationDbContext context) : base(context)
		{
		}

		public OrderRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public IQueryable<CustomerOrderViewModel> GetOrdersViewModel()
		{
			return Context.CustomerOrderViewModels!.AsQueryable();
		}
	}
}
