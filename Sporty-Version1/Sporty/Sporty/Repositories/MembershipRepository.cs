using Microsoft.EntityFrameworkCore;
using Sporty.Data;
using Sporty.Models;
using Sporty.Repositories.Interfaces;

namespace Sporty.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly AppDbContext _context;

        public MembershipRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Membership>> GetAllAsync()
        {
            return await _context.memberships.Include(e => e.User).Include(e=>e.Branch).ToListAsync();
        }

        public async Task<Membership> GetByIdAsync(int id)
        {
            return await _context.memberships.FindAsync(id);
        }

        public async Task AddAsync(Membership membership)
        {
            _context.memberships.Add(membership);
            await _context.SaveChangesAsync();
        }
    }

}
