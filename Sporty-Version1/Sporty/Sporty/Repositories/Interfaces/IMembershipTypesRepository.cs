using Microsoft.EntityFrameworkCore;
using Sporty.Models;

namespace Sporty.Repositories.Interfaces
{
    public interface IMembershipTypesRepository
    {
        public  Task AddAsync(MembershipTypes type);

        public  Task<MembershipTypes> GetTypeByIdAsync(int id);

        public  Task<IEnumerable<MembershipTypes>> GetAllTypesAsync(int BranchId);

        public  Task UpdateBranchAsync(MembershipTypes type);

        public  Task DeleteBranchAsync(int id);

        public  Task<bool> MembershipTypeAvailability(int id);
        
    }
}
