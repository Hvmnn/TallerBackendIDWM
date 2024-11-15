using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWM.Src.Repositories.Implements
{
    public interface IGenderRepository
    {
        Task<IEnumerable<Gender>> GetGenders();
        Task<bool> ValidateGenderId(int id);
    }
}