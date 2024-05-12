using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.Pageable {
    public class PageableRecordRequestDto : PageableBaseRequestDto {
        public Guid? ProcedureId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? AdressId { get; set; }
        public bool? IsClosed { get; set; } = false;
        public bool? IsCanceled { get; set; } = false;

    }
}
