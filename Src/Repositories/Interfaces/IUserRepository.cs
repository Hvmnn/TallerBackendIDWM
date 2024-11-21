using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWM.Src.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> SearchUser(string query);
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByEmail(string email);
        Task<bool> VerifyRut(string rut);
        Task<bool> VerifyEmail(string email);
        Task<bool> VerifyUser(int id);
        Task<bool> AddUser(User user);
        Task<bool> EditUser(int id);
        Task<bool> DelUser(int id);
        Task<bool>ChangeUserState(int id, bool userStatus);
        Task<bool>ChangePassword(int id, string newPassword);
    }
}