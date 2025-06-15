using Sporty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.Repositories.Interfaces
{
    public interface IEventRepo
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task<IEnumerable<Event>> SearchByIdClubAsync(int ClubId);
        Task AddAsync(Event model);
        void Update(Event model);
        void Delete(Event model);

        int Save();
    }
}
