using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.Record {
    public class GetShortedRecordDTO : BaseEntityDto<Guid>{
        public string Detail { get; set; } = string.Empty;
        public DateTime AdmissionDate { get; set; }
        public Guid ProcedureId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployerId { get; set; }
        public Guid AdressId { get; set; }
        public bool IsSpecialOffer { get; set; }
    }
}