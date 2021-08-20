using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Cities
    {
        public Cities()
        {
            Suppliers = new HashSet<Suppliers>();
        }
        //
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Suppliers> Suppliers { get; set; }
    }
}
