using Microsoft.AspNetCore.Identity;

namespace BILab.Domain.Models.Entities {
    public class ApplicationUserRole : IdentityUserRole<Guid> {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
