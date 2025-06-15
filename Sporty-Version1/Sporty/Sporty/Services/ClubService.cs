using Sporty.Models;
using Sporty.Repositories.Interfaces;
using Sporty.Services.Interfaces;

namespace Sporty.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;

        public ClubService(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync()
        {
            // Additional business logic can go here if needed
            return await _clubRepository.GetAllClubsAsync();
        }

        public async Task<Club> GetClubByIdAsync(int id)
        {
            // Additional validation or logic can go here
            return await _clubRepository.GetClubByIdAsync(id);
        }

        public async Task UpdateClubAsync(Club club)
        {
            // Add business validations here if needed before updating
            await _clubRepository.UpdateClubAsync(club);
        }
    }
}
