using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBackendIDWM.Src.DTOs.User
{
    public class EditUserInfoDto
    {
        public string? Name { get; set; }

        public DateTime? Birthday { get; set; }

        public int? GenderId { get; set; } 
    }
}