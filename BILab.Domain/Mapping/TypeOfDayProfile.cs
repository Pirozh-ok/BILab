using AutoMapper;
using BILab.Domain.DTOs.TypeOfDay;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Mapping {
    public class TypeOfDayProfile : Profile {
        public TypeOfDayProfile() {
            CreateMap<TypeOfDay, TypeOfDayDTO>().ReverseMap();
        }
    }
}
