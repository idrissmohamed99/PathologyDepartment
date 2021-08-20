using System;

namespace ExchengeFoodAPI.Models.Supplier
{
    public class BaseSupplierModel
    {
        public string Name { get; set; }
        public string Mather { get; set; }
        public short Gender { get; set; }
        public DateTime DateOfBirth { get; set; } = default;
        public string Phone { get; set; }
        public string IdNumber { get; set; }
        public short TypeOfId { get; set; }
        public string NatId { get; set; }
        public string CityId { get; set; }
        public string Address { get; set; }
    }
}
