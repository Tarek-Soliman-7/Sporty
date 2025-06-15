using Sporty.Models;

namespace Sporty.Repositories.Interfaces
{
    public interface IBookingRepo
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<IEnumerable<Event>> SearchByIdClubAsync(int ClubId);
        Task AddAsync(Booking model);
        void Update(Booking model);
        void Delete(Booking model);

        int Save();
    }
}
