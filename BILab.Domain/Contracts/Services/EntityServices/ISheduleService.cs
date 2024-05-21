﻿using BILab.Domain.Contracts.Services.Base;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Shedule;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Contracts.Services.EntityServices {
    public interface ISheduleService : ISearchableEntityService<Shedule, Guid, SheduleDTO, PageableSheduleRequestDto> {
        Task<ServiceResult> GetFreeShedule(Guid employeeId, DayOfWeek day);
        Task<ServiceResult> GetScheduleByEmployee(Guid employeeId);
    }
}
