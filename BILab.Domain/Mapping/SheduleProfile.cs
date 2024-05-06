using AutoMapper;
using BILab.Domain.DTOs.Shedule;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Mapping {
    public class SheduleProfile : Profile {
        public SheduleProfile() {
            CreateMap<SheduleDTO, Shedule>().ReverseMap();
        }
    }
}
