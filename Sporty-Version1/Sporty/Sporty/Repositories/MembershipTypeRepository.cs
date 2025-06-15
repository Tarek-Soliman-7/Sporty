using Microsoft.EntityFrameworkCore;
using Sporty.Data;
using Sporty.Models;
using Sporty.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporty.Repositories
{
    public class MembershipTypeRepository : IMembershipTypeRepository
    {
        private readonly AppDbContext _context;

        public MembershipTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MembershipTypes>> GetAllAsync(int clubId)
        {
            return await _context.MembershipTypes
                .Include(m => m.Branch)
                .Where(m => m.Branch.ClubId == clubId)
                .ToListAsync();
        }

        public async Task<MembershipTypes> GetByIdAsync(int id)
        {
            return await _context.MembershipTypes
                .Include(m => m.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var membershipType = await GetByIdAsync(id);
            if (membershipType != null)
            {
                _context.MembershipTypes.Remove(membershipType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.MembershipTypes.AnyAsync(m => m.Id == id);
        }

        Task<string?> IMembershipTypeRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
