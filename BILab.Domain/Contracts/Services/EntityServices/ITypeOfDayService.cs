using BILab.Domain.Contracts.Services.Base;
using BILab.Domain.DTOs.TypeOfDay;

namespace BILab.Domain.Contracts.Services.EntityServices {
    public interface ITypeOfDayService : IBaseEntityService<Guid, TypeOfDayDTO>
    {
    }
}
