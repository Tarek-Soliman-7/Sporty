using Sporty.Controllers;
using Sporty.Models;
using Sporty.Repositories;

namespace Sporty.Services
{
    public class MembershipTypesService
    {
        private readonly MembershipTypesRepository _repository;

        public MembershipTypesService (MembershipTypesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.MembershipTypes>>GetAllTypes(int BranchId) {return await _repository.GetAllTypesAsync(BranchId);}
    }
}
