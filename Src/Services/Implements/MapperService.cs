using AutoMapper;
using TallerBackendIDWM.Src.DTOs.Product;
using TallerBackendIDWM.Src.DTOs.Shopping;
using TallerBackendIDWM.Src.DTOs.User;
using TallerBackendIDWM.Src.Models;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Services.Implements
{
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;
        public MapperService(IMapper mapper){
            _mapper = mapper;
        }
        public EditUserInfoDto EditUserDtoToEditUser(EditUserDto editUserDto)
        {
            var mappedUser = _mapper.Map<EditUserInfoDto>(editUserDto);
            return mappedUser;
        }

        public IEnumerable<UserDto> MapUsers(IEnumerable<User> users)
        {
            var mappedUser = users.Select(u => _mapper.Map<UserDto>(u)).ToList();
            return mappedUser;
        }

        public User RegisterClientDtoToUser(RegisterUserDto registerUserDto)
        {
            var mappedUser = _mapper.Map<User>(registerUserDto);
            return mappedUser;
        }

        public UserDto UserToUserDto(User user)
        {
            var mappedUser = _mapper.Map<UserDto>(user);
            return mappedUser;
        }

        public ShoppingCartDto MapShoppingCart(ShoppingCart shoppingCart)
        {
            return _mapper.Map<ShoppingCartDto>(shoppingCart);
        }

        public IEnumerable<CartItemDto> MapCartItems(IEnumerable<CartItem> cartItems)
        {
            return cartItems.Select(ci => _mapper.Map<CartItemDto>(ci)).ToList();
        }

        public ProductDto MapProduct(Product product)
        {
            return _mapper.Map<ProductDto>(product);
        }

        public Product MapProduct(CreateProductDto productDto)
        {
            return _mapper.Map<Product>(productDto);
        }

        public IEnumerable<ProductDto> MapProducts(IEnumerable<Product> products)
        {
            return products.Select(p => _mapper.Map<ProductDto>(p)).ToList();
        }
        
        public SaleDto MapSale(Sale sale)
        {
            return _mapper.Map<SaleDto>(sale);
        }

        public SaleDetailDto MapSaleDetail(Sale sale)
        {
            var saleDetail = _mapper.Map<SaleDetailDto>(sale);
            saleDetail.SaleItems = sale.SaleItems.Select(_mapper.Map<SaleItemDto>).ToList();
            return saleDetail;
        }

        public IEnumerable<SaleDto> MapSales(IEnumerable<Sale> sales)
        {
            return sales.Select(s => _mapper.Map<SaleDto>(s)).ToList();
        }
    }
}