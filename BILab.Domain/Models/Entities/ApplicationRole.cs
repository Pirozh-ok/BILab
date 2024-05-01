using BILab.Domain.Contracts.Models;
using Microsoft.AspNetCore.Identity;

namespace BILab.Domain.Models.Entities {
    public class ApplicationRole : IdentityRole<Guid>, IBaseEntity<Guid> {
        public ApplicationRole() : base() {
        }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
