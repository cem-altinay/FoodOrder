using System;
using System.Collections.Generic;
using FoodOrder.Server.Domain.Common;

namespace FoodOrder.Server.Domain.Entities
{
    public partial class Supplier :BaseEntity
    {
        public string Name { get; set; }
        public string WebUrl { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
