using BILab.Domain.DTOs.Base;

namespace BILab.Domain.Contracts.Services.Base
{
    public interface ISearchableEntityService<TEntity, TKey, TDto, TFilters> : IBaseEntityService<TKey, TDto> {
        ServiceResult<PageableBaseResponseDto<T>> SearchFor<T>(TFilters filters);
    }
}
