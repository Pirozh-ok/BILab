using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.Shedule {
    public class SheduleDTO : BaseEntityDto<Guid>{
        public int FromTime { get; set; }
        public int ToTimeTime { get; set; }

        public Guid UserId { get; set; }
        public Guid TypeOfDayId { get; set; }
    }
}
