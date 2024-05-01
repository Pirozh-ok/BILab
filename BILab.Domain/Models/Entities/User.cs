using BILab.Domain.Contracts.Models;
using Microsoft.AspNetCore.Identity;

namespace BILab.Domain.Models.Entities {
    public class User : IdentityUser<Guid>, IAuditableEntity {
        public User() {
            Shedules = new HashSet<Shedule>();
            UserRoles = new HashSet<ApplicationUserRole>();
            CustomerRecords = new HashSet<Record>();
            EmployeeRecords = new HashSet<Record>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegisterDate { get; set; }
        public Guid? RefreshToken { get; set; }
        public DateTime? ExpirationAt { get; set; }
        public string AvatarPath { get; set; }

        public virtual ICollection<Shedule> Shedules { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
       
        //The records that the client signed up for
        public virtual ICollection<Record> CustomerRecords { get; set; }
        
        //The specialist's records that he works out
        public virtual ICollection<Record> EmployeeRecords { get; set; }
    }
}
