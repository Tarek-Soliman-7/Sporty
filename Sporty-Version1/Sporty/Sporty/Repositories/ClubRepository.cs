using Microsoft.EntityFrameworkCore;
using Sporty.Data;
using Sporty.Models;
using Sporty.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporty.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly AppDbContext _context;

        public ClubRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync()
        {
            return await _context.Clubs.ToListAsync();
        }

        public async Task<Club> GetClubByIdAsync(int id)
        {
            return await _context.Clubs.FindAsync(id);
        }

        //public async Task AddClubAsync(Club club)
        //{
        //    await _context.Clubs.AddAsync(club);
        //    await _context.SaveChangesAsync();
        //}

        public async Task UpdateClubAsync(Club club)
        {
            _context.Clubs.Update(club);
            await _context.SaveChangesAsync();
        }

        //public async Task DeleteClubAsync(int id)
        //{
        //    var club = await GetClubByIdAsync(id);
        //    if (club != null)
        //    {
        //        _context.Clubs.Remove(club);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task<bool> ClubExistsAsync(int id)
        //{
        //    return await _context.Clubs.AnyAsync(c => c.Id == id);
        //}
    }
}
