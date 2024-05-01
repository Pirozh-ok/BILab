using BILab.Domain.Contracts.Models;

namespace BILab.Domain.Models.Entities {
    public class SpecialOffer : IBaseEntity<Guid> {
        public Guid Id { get; set; }
        public string? Detail { get; set; }
        public int Size { get; set; }
    }
}