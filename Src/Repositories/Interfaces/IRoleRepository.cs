
using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWM.Src.Repositories.Implements
{
    public interface IRoleRepository
    {
        Task<bool> ValidateRoleId(int id);
        Task<Role> GetRoleById(int id);
        Task<Role> GetRoleByName(string type);
    }
}