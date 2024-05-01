using BILab.Domain.Contracts.Models;

namespace BILab.Domain.Models.Entities {
    public class TypeOfDay : IBaseEntity<Guid> {
        public TypeOfDay() {
            Shedules = new HashSet<Shedule>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public  virtual ICollection<Shedule> Shedules { get; set; }
    }
}
