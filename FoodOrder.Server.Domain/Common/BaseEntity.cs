using System;
namespace FoodOrder.Server.Domain.Common
{
    public class BaseEntity
    {
        public BaseEntity()
        {
        }

        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string IsActive { get; set; }
    }
}
