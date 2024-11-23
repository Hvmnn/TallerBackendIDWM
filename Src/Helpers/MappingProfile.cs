using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TallerBackendIDWM.Src.DTOs.Shopping;
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
            CreateMap<ShoppingCart, ShoppingCartDto>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.CartItems.Sum(ci => ci.Quantity * ci.Product.Price)));
            
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Quantity * src.Product.Price));
        }
    }
}