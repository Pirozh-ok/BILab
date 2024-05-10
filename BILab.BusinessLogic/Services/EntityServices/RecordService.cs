using AutoMapper;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Record;
using BILab.Domain.Models.Entities;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class RecordService : BaseEntityService<Record, Guid, RecordDTO>, IRecordService {
        public RecordService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) {
        }

        public ServiceResult CloseRecord(Guid recordId, bool isCanceled = false, string? cancelingReason = null) {
            throw new NotImplementedException();
        }

        protected override ServiceResult Validate(RecordDTO dto) {
            return ServiceResult.Fail("not valid");
        }
    }
}
