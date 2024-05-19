using BILab.Domain.Contracts.Models;

namespace BILab.Domain.Models.Entities {
    public class Record : IBaseEntity<Guid> {
        public Guid Id { get; set; }
        public string Detail { get; set; } = string.Empty;
        public DateTime AdmissionDate { get; set; }
        public Guid ProcedureId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployerId { get; set; }
        public Guid AdressId { get; set; }
        public bool IsClosed { get; set; } = false;
        public bool IsCanceled { get; set; } = false;
        public string? CancelingReasone { get; set; }
        public Procedure Procedure { get; set; }
        public User Customer { get; set; }
        public User Employer { get; set; }
        public Adress Adress { get; set; }
    }
}
