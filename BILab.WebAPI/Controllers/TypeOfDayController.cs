using BILab.BusinessLogic.Services.EntityServices;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.TypeOfDay;
using BILab.Domain.Models.Entities;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BILab.WebAPI.Controllers {
    [Authorize(Roles = Constants.NameRoleAdmin)]
    public class TypeOfDayController : BaseCrudController<ITypeOfDayService, TypeOfDayDTO, TypeOfDayDTO, Guid> {
        public TypeOfDayController(ITypeOfDayService service) : base(service) {
        }
    }
}
