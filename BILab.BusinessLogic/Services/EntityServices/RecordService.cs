using AutoMapper;
using AutoMapper.QueryableExtensions;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Record;
using BILab.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class RecordService : SearchableEntityService<RecordService, Record, Guid, RecordDTO, PageableRecordRequestDto>, IRecordService {
        public RecordService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) {
        }

        public async Task<ServiceResult> CloseRecord(Guid recordId, bool isCanceled = false, string? cancelingReason = null) {
            var record = await _context.Records
                .SingleOrDefaultAsync(x => x.Id == recordId);

            if(record is null) {
                return ServiceResult.Fail(ResponseConstants.RecordNotFound);
            }

            if (isCanceled) {
                record.IsClosed = true;
                record.IsCanceled = true;
                record.CancelingReasone = cancelingReason ?? string.Empty;
            }
            else {
                record.IsClosed = true;
            }

            _context.Records.Update(record);

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

            // Detail length
            // AdmissionDate < now and 100 let nazad
            // procedureId procedure is exist
            // customerId customer is exist
            // employeeId employee is exist
            // adressId adress is exist
            //CancelingReasone is not null then check length

            //if (dto is null) {
            //    errors.Add(ResponseConstants.NullArgument);
            //    return BuildValidateResult(errors);
            //}

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