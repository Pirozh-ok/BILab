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

            if (dto is null) {
                errors.Add(ResponseConstants.NullArgument);
                return BuildValidateResult(errors);
            }

            if (dto.FromTime > 24 || dto.FromTime < 0) {
                errors.Add($"From time must be in the range from 0 to 24");
            }

            if (dto.ToTimeTime > 24 || dto.FromTime < 0) {
                errors.Add($"To time must be in the range from 0 to 24");
            }

            if (dto.ToTimeTime < dto.FromTime) {
                errors.Add($"To time must be more than From time");
            }

            if(_context.Users.SingleOrDefault(x => x.Id == dto.UserId) is null) {
                errors.Add($"User not found");
            }

            if (_context.TypesOfDays.SingleOrDefault(x => x.Id == dto.TypeOfDayId) is null) {
                errors.Add($"Type of day not found");
            }

            return BuildValidateResult(errors);
        }
    }
}
