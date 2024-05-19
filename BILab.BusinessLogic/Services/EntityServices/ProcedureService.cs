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

            if (dto.Name.Length > Constants.MaxLenOfName) {
                errors.Add($"Procedure name length must be less then {Constants.MaxLenOfName}");
            }

            if (dto.Name.Length < Constants.MinLenOfName) {
                errors.Add($"Procedure name length must be more then {Constants.MinLenOfName}");
            }

            if (dto.Description.Length > Constants.MaxLenOfDescription) {
                errors.Add($"Procedure description length must be less then {Constants.MaxLenOfDescription} symbols");
            }

            if (dto.Type.Length > Constants.MaxLenOfName || dto.Type.Length < Constants.MinLenOfName) {
                errors.Add($"Type name length must be in the range from {Constants.MinLenOfName} to {Constants.MaxLenOfName}");
            }

            if (dto.Price < 0 || dto.Price > 100000000) {
                errors.Add($"Price size must be in the range from 0 to 100000000");
            }

            if(dto.SpecialOfferId.HasValue && _context.SpecialOffers.SingleOrDefault(x => x.Id == dto.SpecialOfferId) is null) {
                errors.Add($"Special offer not found");
            }

            return BuildValidateResult(errors);
        }
    }
}
