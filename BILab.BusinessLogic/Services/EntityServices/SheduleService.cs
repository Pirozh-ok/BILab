using AutoMapper;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Shedule;
using BILab.Domain.Models.Entities;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class SheduleService : BaseEntityService<Shedule, Guid, SheduleDTO>, ISheduleService {
        public SheduleService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) {
        }

        protected override ServiceResult Validate(SheduleDTO dto) {
            return ServiceResult.Ok();
        }
    }
}
