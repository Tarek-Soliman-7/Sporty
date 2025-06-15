using Microsoft.EntityFrameworkCore;
using Sporty.Data;
using Sporty.Models;
using Sporty.Repositories.Interfaces;

namespace Sporty.Repositories
{
    public class BookingRepo : IBookingRepo
    {
        private readonly AppDbContext _context;

        public BookingRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            //if (typeof(T) == typeof(Employee))
            //{
            //    return (IEnumerable<T>)await Context.Set<Employee>().Include(e => e.Department).ToListAsync();
            //}
            return await _context.Set<Booking>().ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Set<Booking>().FindAsync(id);
        }
        public async Task AddAsync(Booking model)
        {
            await _context.Set<Booking>().AddAsync(model);
            //return Context.SaveChanges();
        }

        public void Update(Booking model)
        {
            _context.Set<Booking>().Update(model);

        }

        public void Delete(Booking model)
        {
            _context.Set<Booking>().Remove(model);

        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<IEnumerable<Event>> SearchByIdClubAsync(int ClubId)
        {
            return await _context.Events.Where(c => c.ClubId == ClubId).ToListAsync();
        }
    }
}
