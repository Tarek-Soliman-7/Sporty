using Microsoft.EntityFrameworkCore;
using Sporty.Data;
using Sporty.Models;
using Sporty.Repositories.Interfaces;

namespace Sporty.Repositories
{
    public class EventRepo:IEventRepo
    {
        private readonly AppDbContext _context;

        public EventRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            //if (typeof(T) == typeof(Employee))
            //{
            //    return (IEnumerable<T>)await Context.Set<Employee>().Include(e => e.Department).ToListAsync();
            //}
            return await _context.Set<Event>().ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Set<Event>().FindAsync(id);
        }
        public async Task AddAsync(Event model)
        {
            await _context.Set<Event>().AddAsync(model);
            //return Context.SaveChanges();
        }

        public void Update(Event model)
        {
            _context.Set<Event>().Update(model);

        }

        public void Delete(Event model)
        {
            _context.Set<Event>().Remove(model);

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
