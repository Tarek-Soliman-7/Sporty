using Microsoft.EntityFrameworkCore;
using Sporty.Data;
using Sporty.Models;
using Sporty.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporty.Services
{
    public class MembershipTypeService 
    {
        private readonly AppDbContext _context;

        public MembershipTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MembershipTypes>> GetAllAsync(int BranchId)
        {
            return await _context.MembershipTypes
                .Where(mt => mt.BranchId == BranchId).ToListAsync();
        }

        public async Task<MembershipTypes> GetByIdAsync(int id)
        {
            return await _context.MembershipTypes
                .Include(mt => mt.Branch)
                .FirstOrDefaultAsync(mt => mt.Id == id);
        }

        public async Task AddAsync(MembershipTypes membershipType)
        {
            await _context.MembershipTypes.AddAsync(membershipType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MembershipTypes membershipType)
        {
            _context.MembershipTypes.Update(membershipType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var membershipType = await _context.MembershipTypes.FindAsync(id);
            if (membershipType != null)
            {
                _context.MembershipTypes.Remove(membershipType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.MembershipTypes.AnyAsync(mt => mt.Id == id);
        }
    }
}
