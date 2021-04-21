using System;
using FoodOrder.Domain.Common;

namespace FoodOrder.Domain.Entities
{
    public partial class OrderItem :BaseEntity
    {
        public Guid CreatedUserId { get; set; }
        public Guid OrderId { get; set; }
        public string Description { get; set; }

        public virtual Order Order { get; set; }
        public virtual Users CreatedUser { get; set; }
    }
}
