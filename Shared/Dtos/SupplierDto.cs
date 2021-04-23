using System;

namespace Shared.Dtos
{
    public class SupplierDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WebUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}