using BILab.BusinessLogic.Services.EntityServices;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Record;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;

namespace BILab.WebAPI.Controllers {
    [Authorize]
    public class RecordController : BaseCrudController<IRecordService, RecordDTO, GetShortedRecordDTO, Guid> {
        public RecordController(RecordService service) : base(service) {
        }

        //get record by user + with filters
        // get record by employer with filters
    }
}
