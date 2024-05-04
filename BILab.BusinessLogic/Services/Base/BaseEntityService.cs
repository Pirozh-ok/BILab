using AutoMapper;
using BILab.Domain.Contracts.Models;
using BILab.Domain;
using BILab.Domain.Contracts.Services.Base;
using Microsoft.EntityFrameworkCore;
using BILab.DataAccess;
using AutoMapper.QueryableExtensions;
using BILab.Domain.DTOs.Base;

namespace BILab.BusinessLogic.Services.Base
{
    public abstract class BaseEntityService<TEntity, TKey, TDto> : BaseService, IBaseEntityService<TKey, TDto>
        where TEntity : class, IBaseEntity<TKey>, new()
        where TDto : BaseEntityDto<TKey>
        where TKey : IEquatable<TKey>, new() {

        public BaseEntityService(
            ApplicationDbContext context,
            IMapper mapper) : base(mapper, context) {
            _dbSet = _context.Set<TEntity>();
        }

        private DbSet<TEntity> _dbSet;

        public virtual async Task<ServiceResult> CreateAsync(TDto dto) {
            var validationResult = Validate(dto);

            if (validationResult.Failure) {
                return ServiceResult.Fail(validationResult.Errors);
            }

            var createObj = BuildEntity(dto);

            _dbSet.Add(createObj);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok(ResponseConstants.Created);
        }

        public virtual async Task<ServiceResult> DeleteAsync(TKey id) {
            var obj = await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (obj is null) {
                return ServiceResult.Fail(ResponseConstants.NotFound);
            }

            _dbSet.Remove(obj);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok(ResponseConstants.Deleted);
        }

        public async virtual Task<ServiceResult> GetAsync<TGetDto>() {
            var objects = await _dbSet
                .AsNoTracking()
                .ProjectTo<TGetDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ServiceResult.Ok(objects);
        }

        public async virtual Task<ServiceResult> GetByIdAsync<TGetDto>(TKey id) {
            var obj = await _dbSet
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            return obj is not null ?
                ServiceResult.Ok(_mapper.Map<TGetDto>(obj)) :
                ServiceResult.Fail(ResponseConstants.NotFound);
        }

        public virtual async Task<ServiceResult> UpdateAsync(TDto dto) {
            var validationResult = Validate(dto);

            if (validationResult.Failure) {
                return ServiceResult.Fail(validationResult.Errors);
            }

            var updateObj = _mapper.Map<TEntity>(dto);

            if (!_dbSet.Any(x => x.Id.Equals(dto.Id))) {
                return ServiceResult.Fail(ResponseConstants.NotFound);
            }

            _dbSet.Update(updateObj);
            await _context.SaveChangesAsync();
            return ServiceResult.Ok(ResponseConstants.Updated);
        }

        protected virtual TEntity BuildEntity(TDto dto) {
            var entity = new TEntity();
            _mapper.Map(dto, entity);
            entity.Id = GetNewKey();

            return entity;
        }

        abstract protected ServiceResult Validate(TDto dto);

        protected virtual IQueryable<TEntity> GetEntityByIdIncludes(IQueryable<TEntity> query) {
            return query;
        }

        protected virtual TKey GetNewKey() {
            return new TKey();
        }

        protected ServiceResult BuildValidateResult(List<string> errors) {
            return errors.Count() > 0 ? ServiceResult.Fail(errors) : ServiceResult.Ok();
        }
    }
}
