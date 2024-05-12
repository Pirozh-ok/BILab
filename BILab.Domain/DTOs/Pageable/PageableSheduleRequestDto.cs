using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.Pageable {
    public class PageableSheduleRequestDto : PageableBaseRequestDto {
        public Guid? UserId { get; set; }
        public Guid? TypeOfDayId { get; set; }
    }
}
