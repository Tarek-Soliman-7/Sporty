using Sporty.Models;

namespace Sporty.Repositories.Interfaces
{
    public interface IMembershipRepository
    {
        Task<IEnumerable<Membership>> GetAllAsync();
        Task<Membership> GetByIdAsync(int id);
        Task AddAsync(Membership membership);
    }

}
