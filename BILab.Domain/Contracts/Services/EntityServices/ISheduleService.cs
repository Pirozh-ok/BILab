using BILab.Domain.Contracts.Services.Base;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Shedule;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Contracts.Services.EntityServices {
    public interface ISheduleService : ISearchableEntityService<Shedule, Guid, SheduleDTO, PageableSheduleRequestDto> {
<<<<<<< HEAD
        Task<ServiceResult> GetFreeShedule(Guid employeeId, DateTime checkData);
=======
        Task<ServiceResult> GetFreeShedule(Guid employeeId, DayOfWeek day);
>>>>>>> bb1ec733aa37ddf2b78cdc966910aaabf16a3964
        Task<ServiceResult> GetScheduleByEmployee(Guid employeeId);
    }
}
