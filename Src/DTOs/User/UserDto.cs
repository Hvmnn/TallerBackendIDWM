using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWM.Src.DTOs.User
{
    public class UserDto
    {
        public int Id {get; set;}
        public string Rut {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public DateTime Birthday {get; set;}
        public string Email {get; set;} = string.Empty;
        public bool IsActive {get; set;}
        public Gender Gender {get; set;} = null!;
        public Role Role {get; set;} = null!;
    }
}