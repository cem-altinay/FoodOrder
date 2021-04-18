﻿using System;
using System.Collections.Generic;
using FoodOrder.Server.Domain.Common;

namespace FoodOrder.Server.Domain.Entities
{
    public partial class Users :BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<OrderItem> CreatedOrderItems { get; set; }
    }
}
