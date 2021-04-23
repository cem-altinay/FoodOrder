using System;

namespace Shared.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        public string CreatedUserFullName {get;set;}
        public string SupplierName { get; set; }
    }
}