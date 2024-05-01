using BILab.Domain.Contracts.Models;

namespace BILab.Domain.Models.Entities {
    public class Adress : IBaseEntity<Guid> {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }

        public virtual ICollection<Record> Records { get; set; }
    }
}
