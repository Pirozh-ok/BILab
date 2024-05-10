using BILab.Domain;
using BILab.Domain.Contracts.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers.Base {
    public class BaseCrudController<TService, TEntityDTO, TGetDto, TKey> : BaseController
    where TService : IBaseEntityService<TKey, TEntityDTO>
    where TKey : IEquatable<TKey> {
        protected TService _service;

        public BaseCrudController(TService service) {
            _service = service;
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync() {
            var result = await _service.GetAsync<TGetDto>();
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByIdAsync(TKey id) {
            var result = await _service.GetByIdAsync<TGetDto>(id);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync([FromBody] TEntityDTO createDto) {
            var result = await _service.CreateAsync(createDto);
            return GetResult(result, (int)HttpStatusCode.Created);
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(TKey id) {
            var result = await _service.DeleteAsync(id);
            return GetResult(result, (int)HttpStatusCode.NoContent);
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync([FromBody] TEntityDTO updateDto) {
            var result = await _service.UpdateAsync(updateDto);
            return GetResult(result, (int)HttpStatusCode.NoContent);
        }
    }
}
