using AutoMapper;
using BILab.Domain.DTOs.SpecialOffer;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Mapping {
    public class SpecialOfferProfile : Profile {
        public SpecialOfferProfile() {
            CreateMap<SpecialOffer, SpecialOfferDTO>().ReverseMap();
        }
    }
}
