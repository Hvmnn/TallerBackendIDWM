using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBackendIDWM.Src.DTOs.User
{
    public class LoggedUserDto
    {
        public required UserDto User {get; set;}
        public required string Token {get; set;}
    }
}