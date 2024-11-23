using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    }
}