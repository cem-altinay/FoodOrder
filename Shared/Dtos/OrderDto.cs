using System;
using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Shared.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid SupplierId { get; set; }
        
        [MinLength(3, ErrorMessage = "Minimum lenght must be 3 characters for Name Field")]
        [StringLength(10)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        public string CreatedUserFullName { get; set; }
        public string SupplierName { get; set; }
    }
}