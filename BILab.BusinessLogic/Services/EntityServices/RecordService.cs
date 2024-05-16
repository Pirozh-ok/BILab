using AutoMapper;
using AutoMapper.QueryableExtensions;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Record;
using BILab.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class RecordService : SearchableEntityService<RecordService, Record, Guid, RecordDTO, PageableRecordRequestDto>, IRecordService {
        private readonly IAccessService _accessService;
        private readonly IEmailService _emailService;

        public RecordService(ApplicationDbContext context,
            IMapper mapper,
            IAccessService accessService,
            IEmailService emailService) : base(context, mapper) {
            _accessService = accessService;
            _emailService = emailService;
        }

        public async Task<ServiceResult> CloseRecord(CloseRecordDto closeRecordDto) {
            var currentUserId = _accessService.GetUserIdFromRequest();
            var record = await _context.Records
                .Include(x => x.Customer)
                .Include(x => x.Procedure)
                .FirstOrDefaultAsync(x => x.Id == closeRecordDto.RecordId);

            if (record is null) {
                return ServiceResult.Fail(ResponseConstants.RecordNotFound);
            }

            if (currentUserId != record.CustomerId && currentUserId != record.EmployerId && !_accessService.IsAdministatorRequest()) {
                return ServiceResult.Fail(ResponseConstants.AccessDenied);
            }

            record.IsClosed = true;

            if (closeRecordDto.IsCanceled) {
                record.IsCanceled = true;
                record.CancelingReasone = closeRecordDto.CancelingReason ?? string.Empty;
            }

            _context.Records.Update(record);

            var customer = record.Customer;
            var emailText = string.Format(ResponseConstants.RecordWasClosedEmail, $"{customer.LastName} {customer.FirstName}", record.Procedure.Name, record.AdmissionDate.ToString("g"));

            await _emailService.SendEmailAsync(customer.Email, ResponseConstants.SubjectCloseRecord, emailText);

            return ServiceResult.Ok(ResponseConstants.RecordWasClosed);
        }

        public async Task<ServiceResult> GetRecordsByEmployeeId(Guid employeeId) {
            var records = await _context.Records
                .Include(x => x.Customer)
                .Include(x => x.Employer)
                .Where(x => x.EmployerId == employeeId)
                .AsNoTracking()
                .ProjectTo<GetFullRecordDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ServiceResult.Ok(records);
        }

        public async Task<ServiceResult> GetRecordsByUserId(Guid userId) {
            var records = await _context.Records
                .Include(x => x.Customer)
                .Include(x => x.Employer)
                .Where(x => x.CustomerId == userId)
                .AsNoTracking()
                .ProjectTo<GetFullRecordDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ServiceResult.Ok(records);
        }

        protected override ServiceResult Validate(RecordDTO dto) {
            var errors = new List<string>();

            if (dto is null) {
                errors.Add(ResponseConstants.NullArgument);
                return BuildValidateResult(errors);
            }

            if (dto.Detail.Length > Constants.MaxLenOfDetail) {
                errors.Add($"Record detail length must be less then {Constants.MaxLenOfDetail}");
            }

            if (dto.AdmissionDate < DateTime.Now || dto.AdmissionDate > DateTime.Now.AddYears(5)) {
                errors.Add($"Admission date is incorrect");
            }

            if (_context.Procedures.SingleOrDefault(x => x.Id == dto.ProcedureId) is null) {
                errors.Add($"Procedure not found");
            }

            if (_context.Users.SingleOrDefault(x => x.Id == dto.EmployerId) is null) {
                errors.Add($"Employee not found");
            }

            if (_context.Users.SingleOrDefault(x => x.Id == dto.CustomerId) is null) {
                errors.Add($"Customer not found");
            }

            if (_context.Adresses.SingleOrDefault(x => x.Id == dto.AdressId) is null) {
                errors.Add($"Adress not found");
            }

            return BuildValidateResult(errors);
        }

        protected override List<Expression<Func<Record, bool>>> GetAdvancedConditions(PageableRecordRequestDto filters) {
            var conditions = new List<Expression<Func<Record, bool>>>();

            if (filters.ProcedureId.HasValue) {
                conditions.Add(x => x.ProcedureId == filters.ProcedureId);
            }

            if (filters.CustomerId.HasValue) {
                conditions.Add(x => x.CustomerId == filters.CustomerId);
            }

            if (filters.CustomerId.HasValue) {
                conditions.Add(x => x.EmployerId == filters.EmployeeId);
            }

            if (filters.AdressId.HasValue) {
                conditions.Add(x => x.AdressId == filters.AdressId);
            }

            if (filters.IsClosed.HasValue) {
                conditions.Add(x => x.IsClosed == filters.IsClosed);
            }

            if (filters.IsCanceled.HasValue) {
                conditions.Add(x => x.IsCanceled == filters.IsCanceled);
            }

            return conditions;
        }
    }
}