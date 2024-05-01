using BILab.Domain.Contracts.Models;

namespace BILab.Domain.Models.Entities {
    public class Shedule : IBaseEntity<Guid> {
        public Guid Id { get; set; }
        public int FromTime { get; set; }
        public int ToTimeTime { get; set; }

        public Guid UserId { get; set; }
        public Guid TypeOfDayId { get; set; }

        public TypeOfDay TypeOfDay { get; set; }
        public User User { get; set; }
    }
}
