using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.Pageable {
    public class PageableProcedureRequestDto : PageableBaseRequestDto {
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
