using AutoMapper;
using BILab.Domain.DTOs.Procedure;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Mapping {
    public class ProcedureProfile : Profile {
        public ProcedureProfile() {
            CreateMap<ProcedureDTO, Procedure>().ReverseMap();
            CreateMap<Procedure, GetProcedureDTO>();
            CreateMap<Procedure, GetSalesDTO>()
                .ForMember(
                dest => dest.NewPrice,
                opt => opt.MapFrom(
                    src => $"{src.SpecialOffer.NewPrice}")
                )
                .ForMember(
                dest => dest.Detail,
                opt => opt.MapFrom(
                    src => $"{src.SpecialOffer.Detail}")
                );
        }
    }
}
