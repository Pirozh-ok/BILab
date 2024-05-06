using AutoMapper;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.TypeOfDay;
using BILab.Domain.Models.Entities;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class TypeOfDayService : BaseEntityService<TypeOfDay, Guid, TypeOfDayDTO>, ITypeOfDayService {
        public TypeOfDayService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) {
        }

        protected override ServiceResult Validate(TypeOfDayDTO dto) {
            var errors = new List<string>();

            if (dto is null) {
                errors.Add(ResponseConstants.NullArgument);
            }


            if (string.IsNullOrEmpty(dto?.Name) || dto.Name.Length < Constants.MinLenOfName) {
                errors.Add(ResponseConstants.NameLessMinLen);
            }

            if (dto?.Name.Length > Constants.MaxLenOfName) {
                errors.Add(ResponseConstants.NameExceedsMaxLen);
            }

            return errors.Count() > 0 ? ServiceResult.Fail(errors) : ServiceResult.Ok();
        }
    }
}
