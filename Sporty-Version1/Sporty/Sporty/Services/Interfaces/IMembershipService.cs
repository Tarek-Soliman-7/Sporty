using Sporty.Models;

namespace Sporty.Services.Interfaces
{
    public interface IMembershipService
    {
        Task<IEnumerable<Membership>> GetAllMembershipsAsync();
        Task<Membership> GetMembershipByIdAsync(int id);
        Task CreateMembershipAsync(Membership membership);
    }

}
