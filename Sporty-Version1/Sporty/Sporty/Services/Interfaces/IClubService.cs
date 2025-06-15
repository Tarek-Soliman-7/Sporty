using Sporty.Models;

namespace Sporty.Services.Interfaces
{
    public interface IClubService
    {
        Task<IEnumerable<Club>> GetAllClubsAsync();
        Task<Club> GetClubByIdAsync(int id);
        Task UpdateClubAsync(Club club);
    }
}
