using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.TypeOfDay {
    public class TypeOfDayDTO  : BaseEntityDto<Guid> {
        public string Name { get; set; }
    }
}
