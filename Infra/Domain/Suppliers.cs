using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Suppliers
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mather { get; set; }
        public short Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CityId { get; set; }
        public string Phone { get; set; }
        public string IdNumber { get; set; }
        public short TypeOfId { get; set; }
        public string NatId { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
        public long Number { get; set; }

        public virtual Cities City { get; set; }
        public virtual Nationality Nat { get; set; }
        public virtual Users User { get; set; }
    }
}
