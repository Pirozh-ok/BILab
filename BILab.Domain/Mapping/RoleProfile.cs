using AutoMapper;
using BILab.Domain.DTOs.Role;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Mapping {
    public class RoleProfile : Profile {
        public RoleProfile() {
            CreateMap<RoleDTO, Role>().ReverseMap();
        }
    }
}
