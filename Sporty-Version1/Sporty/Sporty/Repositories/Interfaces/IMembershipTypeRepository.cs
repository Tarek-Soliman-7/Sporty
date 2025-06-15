using Sporty.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporty.Repositories.Interfaces
{
    public interface IMembershipTypeRepository
    {
        Task<IEnumerable<MembershipTypes>> GetAllAsync(int clubId);
        Task<MembershipTypes> GetByIdAsync(int id);
        Task AddAsync(MembershipTypes membershipType);
        Task UpdateAsync(MembershipTypes membershipType);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<string?> GetAllAsync();
    }
}
