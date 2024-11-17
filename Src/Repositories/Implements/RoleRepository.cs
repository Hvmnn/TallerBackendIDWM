
using Microsoft.EntityFrameworkCore;
using tallerBackendIDWM.Src.Data;
using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWM.Src.Repositories.Implements
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext dataContext){
            _context = dataContext;
        }

        public async Task<Role?> GetRoleById(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            return role;
        }

        public async Task<Role?> GetRoleByName(string type)
        {
            var role = await _context.Roles.Where(r => r.Type == type).FirstOrDefaultAsync();
            return role;
        }

        public async Task<bool> ValidateRoleId(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if(role == null){
                return false;
            }
            return true;
        }
    }
}