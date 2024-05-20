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

        public class Window { 
            public int FromTime { get; init; }
            public int ToTome { get; init; }
        }

        public ServiceResult GetFreeShedule(Guid employeeId, DayOfWeek day) {
            var record = _context.Records
                .Where(x => x.EmployerId == employeeId && x.AdmissionDate.DayOfWeek == day)
                .ToList();

            var scheduleEmployee = _context.Shedules
                .FirstOrDefault(x => x.DayOfWeek == day);

            if(scheduleEmployee is null) {
                return ServiceResult.Fail("Not work in this day");
            }

            var recordHours = record.Select(x => x.AdmissionDate.Hour).ToList();
            var result = new List<Window>();

            for(int i = scheduleEmployee.FromTime; i < scheduleEmployee.ToTimeTime - 1; i++) {
                if (!recordHours.Contains(i)) {
                    result.Add(new Window() {
                        FromTime = i,
                        ToTome = i + 1
                    });
                }
            }

            return ServiceResult.Ok(new List<Window>());
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

            if ((int)dto.DayOfWeek < 0 || (int)dto.DayOfWeek > 6) {
                errors.Add(ResponseConstants.IncorrectedDay);
            }

            return BuildValidateResult(errors);
        }
    }
}
