using AutoMapper;
using BILab.Domain.DTOs.Record;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Mapping {
    public class RecordProfile : Profile {
        public RecordProfile() {
            CreateMap<RecordDTO, Record>().ReverseMap();
            CreateMap<Record, GetFullRecordDTO>()
                .ForMember(
                dest => dest.Adress,
                opt => opt.MapFrom(
                    src => $"{src.Adress.City}, {src.Adress.Street}, {src.Adress.HouseNumber}, {src.Adress.ApartmentNumber}")
                )
                .ForMember(
                dest => dest.IsSpecialOffer,
                opt => opt.MapFrom(
                    src => src.Procedure.SpecialOfferId != null)
                );
            CreateMap<Record, GetShortedRecordDTO>()
                .ForMember(
                dest => dest.IsSpecialOffer,
                opt => opt.MapFrom(
                    src => src.Procedure.SpecialOfferId != null)
                );
        }
    }
}
