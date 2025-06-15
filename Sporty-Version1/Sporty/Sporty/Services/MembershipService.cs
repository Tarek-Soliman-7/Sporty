using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sporty.Models;
using Sporty.Repositories;
using Sporty.Repositories.Interfaces;
using Sporty.Services.Interfaces;
namespace Sporty.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _repository;

        public MembershipService(IMembershipRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Membership>> GetAllMembershipsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Membership> GetMembershipByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateMembershipAsync(Membership membership)
        {
            await _repository.AddAsync(membership);
        }
    }

}
