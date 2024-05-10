using BILab.Domain.DTOs.Base;
using BILab.Domain.DTOs.Procedure;
using BILab.Domain.DTOs.User;

namespace BILab.Domain.DTOs.Record {
    public class GetFullRecordDTO : BaseEntityDto<Guid>{
        public string Detail { get; set; } = string.Empty;
        public DateTime AdmissionDate { get; set; }
        public GetProcedureDTO Procedure { get; set; }
        public GetShortUserInfoDTO Customer { get; set; }
        public GetShortUserInfoDTO Employer { get; set; }
        public string Adress { get; set; }
        public bool IsClosed { get; set; } = false;
        public bool IsCanceled { get; set; } = false;
        public string? CancelingReasone { get; set; }
    }
}
