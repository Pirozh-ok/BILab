using BILab.Domain.Contracts.Models;
using Microsoft.AspNetCore.Identity;

namespace BILab.Domain.Models.Entities {
    public class Role : IdentityRole<Guid>, IBaseEntity<Guid> {
        public Role() : base() {
        }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
