using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Roles
    {
        public Roles()
        {
            RolePermisstion = new HashSet<RolePermisstion>();
            Users = new HashSet<Users>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public string CreateUserId { get; set; }

        public virtual Users CreateUser { get; set; }
        public virtual ICollection<RolePermisstion> RolePermisstion { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
