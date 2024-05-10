using BILab.Domain.Contracts.Services.Base;
using BILab.Domain.DTOs.Record;

namespace BILab.Domain.Contracts.Services.EntityServices {
    public interface IRecordService : IBaseEntityService<Guid, RecordDTO>
    {
        public ServiceResult CloseRecord(Guid recordId, bool isCanceled = false, string? cancelingReason = null);
    }
}
