using BILab.Domain.Contracts.Services.Base;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Record;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Contracts.Services.EntityServices {
    public interface IRecordService : ISearchableEntityService<Record, Guid, RecordDTO, PageableRecordRequestDto>
    {
        public Task<ServiceResult> CloseRecord(Guid recordId, bool isCanceled = false, string? cancelingReason = null);
        public Task<ServiceResult> GetRecordsByEmployeeId(Guid employeeId);
        public Task<ServiceResult> GetRecordsByUserId(Guid userId);
    }
}
