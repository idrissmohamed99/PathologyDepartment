using System;

namespace Infra.DTOs.SuppliersDTO
{
    public class SupplierDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mather { get; set; }
        public short Gender { get; set; }
        public string GenderName { get; set; }
        public string CityName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string IdNumber { get; set; }
        public short TypeOfId { get; set; }
        public string NationalityName { get; set; }

        public string Address { get; set; }
        public string UserId { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public long Number { get; set; }
    }
}
