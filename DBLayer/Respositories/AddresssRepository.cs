using DBLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBLayer
{
    public class AddresssRepository
    {
        private P2DbContext _context;

        public AddresssRepository(P2DbContext context)
        {
            _context = context;
        }

        public async Task<List<Addresss>> GetAll()
        {
            return await _context.Set<Addresss>().ToListAsync();
        }
    }
}