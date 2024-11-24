using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBackendIDWM.Src.DTOs.User;
using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWM.Src.Services.Interface
{
    public interface IUserService
    {
        Task<bool> ChangeUserPassword(int id, ChangePasswordDto changePasswordDto);
        Task<bool> EditUser(int id, EditUserDto editUserDto);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<IEnumerable<UserDto>> SearchUsers(string query);
        Task<IEnumerable<Gender>> GetGenders();
        Task<bool> ChangeUserState(int id, bool userState);
        Task<bool> DeleteUser(int id);
    }
}