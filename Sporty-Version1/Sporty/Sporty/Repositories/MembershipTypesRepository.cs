using Microsoft.EntityFrameworkCore;
using Sporty.Data;
using Sporty.Models;
using Sporty.Repositories.Interfaces;
namespace Sporty.Repositories
{
    public class MembershipTypesRepository:IMembershipTypesRepository
    {
        private readonly AppDbContext _context;

        public MembershipTypesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async  Task AddAsync(MembershipTypes type)
        {
           await  _context.MembershipTypes.AddAsync(type);
            _context.SaveChanges();
        }

        public async Task<MembershipTypes> GetTypeByIdAsync(int id)
        {
            return await _context.MembershipTypes.FindAsync(id);

        }

        public async Task<IEnumerable<MembershipTypes>> GetAllTypesAsync(int BranchId)
        {
            return await _context.MembershipTypes
               .Where(b => b.BranchId == BranchId)
               .ToListAsync();
        }

        public async Task UpdateBranchAsync(MembershipTypes type)
        {
            _context.MembershipTypes.Update(type);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteBranchAsync(int id)
        {
            var type = await GetTypeByIdAsync(id);
            if (type != null)
            {
                _context.MembershipTypes.Remove(type);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> MembershipTypeAvailability(int id)
        {
           MembershipTypes type = await GetTypeByIdAsync(id);
            return type.availability;
        }
    }
}
