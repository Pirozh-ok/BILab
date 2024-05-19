using BILab.Domain.Contracts.Models;

namespace BILab.Domain.Models.Entities {
    public class Procedure : IBaseEntity<Guid> {
        public Procedure() {
            Records = new HashSet<Record>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public Guid? SpecialOfferId { get; set; }
        public string? Picture { get; set; }

        public SpecialOffer? SpecialOffer { get; set; }
        public ICollection<Record> Records { get; set; }
    }
}