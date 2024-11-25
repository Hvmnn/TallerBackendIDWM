using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBackendIDWM.Src.DTOs.User;

namespace TallerBackendIDWM.Src.Services.Interface
{
    public interface IAuthService
    {
        Task<LoggedUserDto> RegisterUser(RegisterUserDto registerUserDto);
        Task<LoggedUserDto> Login(LoginUserDto loginUserDto);
    }
}