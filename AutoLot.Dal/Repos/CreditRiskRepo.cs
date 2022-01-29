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
	internal class CreditRiskRepo : BaseRepo<CreditRisk>, ICreditRiskRepo
	{
		public CreditRiskRepo(ApplicationDbContext context) : base(context)
		{
		}
		public CreditRiskRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
	}
}
