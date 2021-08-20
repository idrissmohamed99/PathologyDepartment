using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Modules
    {
        public Modules()
        {
            Users = new HashSet<Users>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
