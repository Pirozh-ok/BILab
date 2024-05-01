using BILab.Domain.Contracts.Models;

namespace BILab.Domain.Models.Entities {
    public class SpecialOffer : IBaseEntity<Guid> {
        public SpecialOffer()
        {
            Procedures = new HashSet<Procedure>();
        }

        public Guid Id { get; set; }
        public string? Detail { get; set; }
        public int Size { get; set; }

        public virtual ICollection<Procedure> Procedures { get; set; }
    }
}