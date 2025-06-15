using Sporty.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporty.Repositories.Interfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetAllBranchesAsync(int id);
        Task<IEnumerable<Branch>> GetAllBranchesAsync();
        Task<Branch> GetBranchByIdAsync(int id);
        Task<Branch> GetBranchWithClubAsync(int id);
        Task AddBranchAsync(Branch branch);
        Task UpdateBranchAsync(Branch branch);
        Task DeleteBranchAsync(int id);
        Task<bool> BranchExistsAsync(int id);
    }
}
