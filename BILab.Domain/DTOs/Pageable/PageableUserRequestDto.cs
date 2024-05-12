using BILab.Domain.DTOs.Base;
using BILab.Domain.Enums;

namespace BILab.Domain.DTOs.Pageable {
    public class PageableUserRequestDto : PageableBaseRequestDto {
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public Sex? Sex { get; set; }
    }
}
