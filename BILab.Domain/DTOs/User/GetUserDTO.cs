using BILab.Domain.DTOs.Base;
using BILab.Domain.Enums;

namespace BILab.Domain.DTOs.User {
    public class GetUserDTO : BaseEntityDto<Guid> {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? AvatarPath { get; set; }
        public Sex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfRegistration { get; set; }
    }
}
