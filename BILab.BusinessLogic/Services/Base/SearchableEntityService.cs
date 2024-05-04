using AutoMapper;
using BILab.Domain.Contracts.Models;
using BILab.Domain.Contracts.Services.Base;
using BILab.Domain.DTOs.Base;
using BILab.Domain;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.QueryableExtensions;
using BILab.DataAccess;

namespace BILab.BusinessLogic.Services.Base {
    public abstract class SearchableEntityService<TService, TEntity, TKey, TDto, TFilters> : BaseEntityService<TEntity, TKey, TDto>,
        ISearchableEntityService<TEntity, TKey, TDto, TFilters>
        where TEntity : class, IBaseEntity<TKey>, new()
        where TDto : BaseEntityDto<TKey>
        where TFilters : PageableBaseRequestDto
        where TKey : IEquatable<TKey>, new() {

        public SearchableEntityService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { 
        }

        public ServiceResult<PageableBaseResponseDto<T>> SearchFor<T>(TFilters filters) {
            try {
                var query = _context.Set<TEntity>().AsQueryable();

                query = ApplyConditions(query, GetGeneralConditions(filters));

                var totalItems = query.Count();

                query = ApplyConditions(query, GetAdvancedConditions(filters));

                query = OrderByField(query, filters?.OrderBy?.ToLower(), filters.DescOrder);

                query = SearchForIncludes(query, filters);

                var filteredItems = query.Count();

                query = ApplyPaging(query, filters.Take, filters.Skip);

                return ServiceResult.Ok(new PageableBaseResponseDto<T> {
                    TotalItems = totalItems,
                    FilteredItems = filteredItems,
                    Items = query
                    .ProjectTo<T>(_mapper.ConfigurationProvider)
                    .ToList()
                });
            }
            catch (Exception ex) {
                return ServiceResult<PageableBaseResponseDto<T>>.InternalServerError();
            }
        }

        protected virtual List<Expression<Func<TEntity, bool>>> GetGeneralConditions(TFilters filters) {
            return null;
        }

        protected virtual List<Expression<Func<TEntity, bool>>> GetAdvancedConditions(TFilters filters) {
            return null;
        }

        protected virtual IQueryable<TEntity> OrderByField(IQueryable<TEntity> query, string fieldName, bool desc) {
            if (string.IsNullOrEmpty(fieldName)) {
                return query;
            }

            try {
                string command = desc ? "OrderByDescending" : "OrderBy";
                var type = typeof(TEntity);
                var defaultFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
                var property = type.GetProperty(fieldName, defaultFlags | BindingFlags.IgnoreCase);
                var parameter = Expression.Parameter(type, property.ToString());
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] {
                type, property.PropertyType }, query.Expression, Expression.Quote(orderByExpression));

                return query.Provider.CreateQuery<TEntity>(resultExpression);
            }
            catch (Exception ex) {
                return query;
            }
        }

        protected virtual IQueryable<TEntity> SearchForIncludes(IQueryable<TEntity> query, TFilters filters) {
            return GetEntityByIdIncludes(query);
        }

        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, int take, int skip) {
            query = query.Skip(skip);
            if (take > 0) {
                query = query.Take(take);
            }

            return query;
        }

        private IQueryable<TEntity> ApplyConditions(IQueryable<TEntity> query, List<Expression<Func<TEntity, bool>>> conditions) {
            if (conditions != null) {
                foreach (var condition in conditions) {
                    query = query.Where(condition);
                }
            }

            return query;
        }
    }
}