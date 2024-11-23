using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TallerBackendIDWM.Src.DTOs.User;
using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWM.Src.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto,User>();
            CreateMap<EditUserDto, EditUserInfoDto>();
        }
    }
}