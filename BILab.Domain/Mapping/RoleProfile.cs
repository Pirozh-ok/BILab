using AutoMapper;
using BILab.Domain.DTOs.Role;
using BILab.Domain.Models.Entities;
using KinoPoisk.DomainLayer.DTOs.RoleDto;

namespace BILab.Domain.Mapping {
    public class RoleProfile : Profile {
        public RoleProfile() {
            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<Role, GetRoleDto>();
        }
    }
}
