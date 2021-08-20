using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Nationality
    {
        public Nationality()
        {
            Suppliers = new HashSet<Suppliers>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Suppliers> Suppliers { get; set; }
    }
}
