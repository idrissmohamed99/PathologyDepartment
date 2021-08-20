using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Users
    {
        public Users()
        {
            Roles = new HashSet<Roles>();
            Suppliers = new HashSet<Suppliers>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public string CreateUserId { get; set; }
        public string RoleId { get; set; }
        public string FullName { get; set; }
        public string GroupId { get; set; }
        public bool? IsSendEmail { get; set; }

        public virtual Modules Group { get; set; }
        public virtual Roles Role { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
        public virtual ICollection<Suppliers> Suppliers { get; set; }
    }
}
