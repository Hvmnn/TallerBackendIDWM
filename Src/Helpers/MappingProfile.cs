using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TallerBackendIDWM.Src.DTOs.Product;
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
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.CartItems.Sum(ci => ci.Quantity * ci.Product.Price)))
                .ReverseMap();
            
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Quantity * src.Product.Price))
                .ReverseMap();
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            CreateMap<Sale, SaleDetailDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            CreateMap<SaleItem, SaleItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Total));
        }
    }
}