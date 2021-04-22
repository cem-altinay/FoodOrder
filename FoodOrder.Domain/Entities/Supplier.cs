using System.Collections.Generic;
using FoodOrder.Domain.Common;

namespace FoodOrder.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public string WebUrl { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}