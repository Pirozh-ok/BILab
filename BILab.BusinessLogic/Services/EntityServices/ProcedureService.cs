using AutoMapper;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Procedure;
using BILab.Domain.Models.Entities;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class ProcedureService : BaseEntityService<Procedure, Guid, ProcedureDTO>, IProcedureService {
        public ProcedureService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) {
        }

        protected override ServiceResult Validate(ProcedureDTO dto) {
            return ServiceResult.Fail("Error validation");
        }
    }
}
