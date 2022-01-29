using AutoLot.Dal.EfStructures;
using AutoLot.Dal.Repos.Base;
using AutoLot.Dal.Repos.Interfaces;
using AutoLot.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Repos
{
	internal class MakeRepo : BaseRepo<Make>, IMakeRepo
	{
		public MakeRepo(ApplicationDbContext context) : base(context)
		{
		}

		public MakeRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public override IEnumerable<Make> GetAll()
			=> Table.OrderBy(x => x.Name);

		public override IEnumerable<Make> GetAllIgnoreQueryFilters()
			=> Table.IgnoreQueryFilters().OrderBy(x => x.Name);
	}
}
