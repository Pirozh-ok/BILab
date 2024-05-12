using AutoMapper;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Shedule;
using BILab.Domain.Models.Entities;
using System.Linq.Expressions;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class SheduleService : SearchableEntityService<SheduleService, Shedule, Guid, SheduleDTO, PageableSheduleRequestDto>, ISheduleService {
        public SheduleService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) {
        }

        protected override List<Expression<Func<Shedule, bool>>> GetAdvancedConditions(PageableSheduleRequestDto filters) {
            var conditions = new List<Expression<Func<Shedule, bool>>>();

            if (filters.UserId.HasValue) {
                conditions.Add(x => x.UserId == filters.UserId);
            }

            if (filters.TypeOfDayId.HasValue) {
                conditions.Add(x => x.TypeOfDayId ==  filters.TypeOfDayId);
            }

            return conditions;
        }

        protected override ServiceResult Validate(SheduleDTO dto) {
            var errors = new List<string>();

            //FromTime > 0 < 24 and ToTimeTime
            // UserId user is not null
            // TypeOfDayId TypeOfDay is not null

            if (dto is null) {
                errors.Add(ResponseConstants.NullArgument);
                return BuildValidateResult(errors);
            }

            //if (dto.FromTime > ) {
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
