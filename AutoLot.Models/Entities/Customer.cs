using AutoLot.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLot.Models.Entities
{
    [Table("Customers", Schema = "dbo")]
    public partial class Customer : BaseEntity
    {
        public Customer()
        {
            CreditRisks = new HashSet<CreditRisk>();
            Orders = new HashSet<Order>();
        }
            
        [InverseProperty(nameof(CreditRisk.Customer))]
        public ICollection<CreditRisk> CreditRisks { get; set; }
        [InverseProperty(nameof(Order.Customer))]
        public ICollection<Order> Orders { get; set; }
    }
}
