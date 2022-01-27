using AutoLot.Models.Entities.Base;
using AutoLot.Models.Entities.Owned;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoLot.Models.Entities
{
    [Table("Customers", Schema = "dbo")]
    public partial class Customer : BaseEntity
    {
        
        [JsonIgnore]
        [InverseProperty(nameof(CreditRisk.CustomerNavigation))]
        public IEnumerable<CreditRisk> CreditRisks { get; set; } = new List<CreditRisk>();
        
        [JsonIgnore]
        [InverseProperty(nameof(Order.CustomerNavigation))]
        public IEnumerable<Order> Orders { get; set; } = new HashSet<Order>();

        public Person PersonalInformation { get; set; } = new Person(); //owned
    }
}
