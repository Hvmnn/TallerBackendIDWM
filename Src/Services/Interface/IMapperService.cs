using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBackendIDWM.Src.DTOs;
using TallerBackendIDWM.Src.DTOs.Product;
using TallerBackendIDWM.Src.DTOs.Shopping;
using TallerBackendIDWM.Src.DTOs.User;
using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWM.Src.Services.Interface
{
    public interface IMapperService
    {
        public IEnumerable<UserDto> MapUsers(IEnumerable<User> users);
        public User RegisterClientDtoToUser(RegisterUserDto registerUserDto);
        public UserDto UserToUserDto(User user);
        public EditUserInfoDto EditUserDtoToEditUser(EditUserDto editUserDto);
        public ShoppingCartDto MapShoppingCart(ShoppingCart shoppingCart);
        public IEnumerable<CartItemDto> MapCartItems(IEnumerable<CartItem> cartItems);
        public ProductDto MapProduct(Product product);
        public Product MapProduct(CreateProductDto productDto);
        public IEnumerable<ProductDto> MapProducts(IEnumerable<Product> products);
        public SaleDto MapSale(Sale sale);
        public SaleDetailDto MapSaleDetail(Sale sale);
        public IEnumerable<SaleDto> MapSales(IEnumerable<Sale> sales);

    }
}