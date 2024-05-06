using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.Role {
    public class RoleDTO : BaseEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
