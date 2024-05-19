using BILab.Domain.Contracts.Services.Base;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Procedure;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Contracts.Services.EntityServices {
    public interface IProcedureService : ISearchableEntityService<Procedure, Guid, ProcedureDTO, PageableProcedureRequestDto> {
        public Task<ServiceResult> GetSaleByIdAsync(Guid saleId);
        public Task<ServiceResult> GetSalesAsync();
    }
}
