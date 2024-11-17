using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tallerBackendIDWM.Src.Data;
using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWM.Src.Repositories.Implements
{
    public class GenderRepository : IGenderRepository
    {
        private readonly DataContext _context;
        public GenderRepository(DataContext dataContext){
            _context = dataContext;
        }
        public async Task<IEnumerable<Gender>> GetGenders()
        {
            var genders = await _context.Genders.ToListAsync();
            return genders;
        }

        public async Task<bool> ValidateGenderId(int id)
        {
            var existingGender = await _context.Genders.FindAsync(id);
            if(existingGender == null){
                return false;
            }
            return true;
        }
    }
}