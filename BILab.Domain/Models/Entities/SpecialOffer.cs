using BILab.Domain.Contracts.Models;

namespace BILab.Domain.Models.Entities {
    public class SpecialOffer : IBaseEntity<Guid> {
        public SpecialOffer() {
        }

        public Guid Id { get; set; }
        public string? Detail { get; set; }
        public int NewPrice { get; set; }
        public Guid? ProcedureId { get; set; }

        public Procedure? Procedure {get; set;}
    }
}