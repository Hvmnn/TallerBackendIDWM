using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace TallerBackendIDWM.Src.Repositories.Implements
{
    public interface IRoleRepository
    {
        Task<bool> ValidateRoleId(int id);
        Task<Role> GetRoleById(int id);
        Task<Role> GetRoleByName(string type);
    }
}