using BILab.Domain.Contracts.Models;
using BILab.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace BILab.Domain.Models.Entities {
    public class User : IdentityUser<Guid>, IBaseEntity<Guid> {
        public User() {
            Shedules = new HashSet<Shedule>();
            UserRoles = new HashSet<ApplicationUserRole>();
            CustomerRecords = new HashSet<Record>();
            EmployeeRecords = new HashSet<Record>();

            UserName = $"user{DateTime.Now.Millisecond}{DateTime.Now.Second}";
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; } = Sex.Undefined;
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime RegisterDate { get; set; }
        public Guid? RefreshToken { get; set; }
        public DateTime? ExpirationAt { get; set; }
        public string? AvatarPath { get; set; }

        public virtual ICollection<Shedule> Shedules { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
       
        //The records that the client signed up for
        public virtual ICollection<Record> CustomerRecords { get; set; }
        
        //The specialist's records that he works out
        public virtual ICollection<Record> EmployeeRecords { get; set; }
    }
}
