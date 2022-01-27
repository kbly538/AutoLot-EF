using System;
using System.Collections.Generic;

namespace AutoLot.Models.Entities
{
    public partial class Make
    {
        public Make()
        {
            Inventories = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte[]? TimeStamp { get; set; }

        public virtual ICollection<Car> Inventories { get; set; }
    }
}
