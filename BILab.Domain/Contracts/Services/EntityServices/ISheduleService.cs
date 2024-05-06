using BILab.Domain.Contracts.Services.Base;
using BILab.Domain.DTOs.Shedule;

namespace BILab.Domain.Contracts.Services.EntityServices {
    public interface ISheduleService : IBaseEntityService<Guid, SheduleDTO>
    {
    }
}
