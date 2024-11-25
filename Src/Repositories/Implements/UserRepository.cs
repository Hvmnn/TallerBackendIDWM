using Microsoft.EntityFrameworkCore;
using TallerBackendIDWM.Src.Data;
using TallerBackendIDWM.Src.DTOs.User;
using TallerBackendIDWM.Src.Models;
using TallerBackendIDWM.Src.Repositories.Interfaces;

namespace TallerBackendIDWM.Src.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<bool> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePassword(int id, string newPassword)
        {
            var user = await _context.Users.FindAsync(id);
            if(user==null){
                return false;
            }
            user.Password = newPassword;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeUserState(int id, bool userStatus)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null){
                return false;
            }
            user.IsActive = userStatus;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DelUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null){
                return false;
            }
            
            _context.Entry(user).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditUser(int id, EditUserInfoDto editUserDto)
        {
            var existUser = await _context.Users.FindAsync(id);
            if(existUser == null){
                return false;
            }

            existUser.Name = editUserDto.Name ?? existUser.Name;
            existUser.Birthday = editUserDto.Birthday ?? existUser.Birthday;
            existUser.GenderId = editUserDto.GenderId ?? existUser.GenderId;


            _context.Entry(existUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email)
                                            .Include(u => u.Role)
                                            .Include(u => u.Gender)
                                            .FirstOrDefaultAsync();
            
            return user;
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = await _context.Users.Where(u => u.Id == id)
                                                .Include(u => u.Role)
                                                .Include(u => u.Gender)
                                                .FirstOrDefaultAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(u => u.Role)
                                            .Include(u => u.Gender)
                                            .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<User>> SearchUser(string query)
        {
            var users = await _context.Users.Where(u => u.Id.ToString().Contains(query)
                                            || u.Rut.Contains(query)
                                            || u.Name.Contains(query)
                                            || u.Birthday.ToString().Contains(query)
                                            || u.Email.Contains(query)
                                            || u.IsActive.ToString().Contains(query)
                                            || u.Gender.Type.Contains(query))
                                            .Include(u => u.Role)
                                            .Include(u => u.Gender)
                                            .ToListAsync();

            return users;
        }

        public async Task<bool> VerifyEmail(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
        }

        public async Task<bool> VerifyRut(string rut)
        {
            var user = await _context.Users.Where(u => u.Rut == rut).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
        }

        public async Task<bool> VerifyUser(int id)
        {
            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
        }
    }
}