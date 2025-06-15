using Sporty.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporty.Repositories.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAllClubsAsync();
        Task<Club> GetClubByIdAsync(int id);
        //Task AddClubAsync(Club club);
        Task UpdateClubAsync(Club club);
        //Task DeleteClubAsync(int id);
        //Task<bool> ClubExistsAsync(int id);
    }
}
