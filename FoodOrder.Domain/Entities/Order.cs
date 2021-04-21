using System;
using System.Collections.Generic;
using FoodOrder.Domain.Common;

namespace FoodOrder.Domain.Entities
{
    public partial class Order :BaseEntity
    {
        public Guid CreatedUserId { get; set; }
        public Guid SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }

        public virtual  ICollection<OrderItem> OrderItems { get; set; }
        public virtual Users CreatedUser { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
