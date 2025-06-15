using Microsoft.AspNetCore.Identity;
using Sporty.Models;
using Sporty.Repositories.Interfaces;
using Sporty.ViewModels;

namespace Sporty.Services
{
    public class BranchService
    {
        private readonly IBranchRepository _branchRepository;


        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;

        }

        public async Task<IEnumerable<Branch>> GetAllBranches(int ClubId) { return await _branchRepository.GetAllBranchesAsync(ClubId); }

        public async Task<Branch>GetBranchWithClubID(int Id) { return await  _branchRepository.GetBranchWithClubAsync(Id);    }

        public async Task AddBranch(BranchViewModel model)
        {
            var branch = new Branch
            {
                Name = model.Name,
                Location = model.Location,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                ClubId = model.ClubId
            };

            await _branchRepository.AddBranchAsync(branch);
        }

        public async Task<Branch> GetBranch(int id)
        {
           return  await _branchRepository.GetBranchByIdAsync(id);
        }

        public async Task UpdateBranch(Branch branch)
        {
             await _branchRepository.UpdateBranchAsync(branch);
        }

        public async Task DeleteBranch(int id)
        {
            await _branchRepository.DeleteBranchAsync(id);
        }

    }
}
