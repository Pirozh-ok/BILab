using AutoMapper;
using BILab.Domain.DTOs.User;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Mapping {
    public class UserProfile : Profile {
        public UserProfile() {
            CreateMap<UserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<User, GetUserDTO>();
            CreateMap<UserDTO, UpdateUserDTO>();
            CreateMap<User, GetShortUserInfoDTO>();
        }
    }
}
