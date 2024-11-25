using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBackendIDWM.Src.Services.Interface
{
    public interface ITokenService
    {
        public int GetUserIdFromToken();
    }
}