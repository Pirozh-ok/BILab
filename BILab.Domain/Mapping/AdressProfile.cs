using AutoMapper;
using BILab.Domain.DTOs.Adress;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Mapping {
    public class AdressProfile : Profile{
        public AdressProfile() {
            CreateMap<AdressDTO, Adress>().ReverseMap();
        }
    }
}
