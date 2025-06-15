using Microsoft.EntityFrameworkCore;
using Sporty.Data;
using Sporty.Models;
using Sporty.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporty.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDbContext _context;

        public BranchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Branch>> GetAllBranchesAsync(int ClubId)
        {
             return await _context.Branches
                .Where(b => b.ClubId==ClubId)
                .ToListAsync();
        }

        public async Task<Branch> GetBranchByIdAsync(int id)
        {
            return await _context.Branches.FindAsync(id);
        }

        public async Task<Branch> GetBranchWithClubAsync(int id)
        {
            return await _context.Branches
                .Include(b => b.Club)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBranchAsync(Branch branch)
        {
            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBranchAsync(Branch branch)
        {
            _context.Branches.Update(branch);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBranchAsync(int id)
        {
            var branch = await GetBranchByIdAsync(id);
            if (branch != null)
            {
                _context.Branches.Remove(branch);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> BranchExistsAsync(int id)
        {
            return await _context.Branches.AnyAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Branch>> GetAllBranchesAsync()
        {
            return await _context.Branches.ToListAsync();
        }
    }
}
