using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.User {
    public class UpdateUserDTO : BaseEntityDto <Guid> {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
