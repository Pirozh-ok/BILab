using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.User {
    public class GetShortUserInfoDTO : BaseEntityDto<Guid> {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? AvatarPath { get; set; }
    }
}
