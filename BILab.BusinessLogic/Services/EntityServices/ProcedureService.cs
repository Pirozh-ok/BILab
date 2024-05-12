using AutoMapper;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Procedure;
using BILab.Domain.Models.Entities;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class ProcedureService : SearchableEntityService<ProcedureService, Procedure, Guid, ProcedureDTO, PageableProcedureRequestDto>, IProcedureService {
        public ProcedureService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) {
        }

        protected override ServiceResult Validate(ProcedureDTO dto) {
            var errors = new List<string>();

            if (dto is null) {
                errors.Add(ResponseConstants.NullArgument);
                return BuildValidateResult(errors);
            }

            //name length
            //description length
            //type length
            //price > 0 < 100000000000000000000
            // specialOfferId is exist

            //if (dto.Size < Constants.MinSpecialOffer) {
            //    errors.Add($"Special offer size must be more then {MinSpecialOffer}");
            //}

            //if (dto.Size > Constants.MaxSpecialOffer) {
            //    errors.Add($"Special offer size must be less then {Constants.MaxSpecialOffer}");
            //}

            //if (dto.Detail != null && dto.Detail.Length > Constants.MaxLenOfDetail) {
            //    errors.Add($"Special offer detail length must be less then {Constants.MaxLenOfDetail}");
            //}

            return BuildValidateResult(errors);
        }
    }
}
