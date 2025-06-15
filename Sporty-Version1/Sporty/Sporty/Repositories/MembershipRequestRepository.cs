using Microsoft.EntityFrameworkCore;
using Sporty.Data;
using Sporty.Models;

namespace Sporty.Repositories
{
    public class MembershipRequestRepository
    {
        private readonly AppDbContext _Context;

        public MembershipRequestRepository(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task AddRequest(MembershipRequests request)
        {
            await _Context.MembershipRequests.AddAsync(request);
            await _Context.SaveChangesAsync();
        }
        public void UpdateRequest(MembershipRequests request)
        {
            _Context.MembershipRequests.Update(request);
             _Context.SaveChanges();
        }

        public async Task<MembershipRequests> GetRequestById(int Id)
        {
            return await _Context.MembershipRequests.FirstOrDefaultAsync(e => e.Id == Id);
        }
        public  MembershipRequests GetRequestByIdSync(int Id)
        {
            return  _Context.MembershipRequests.FirstOrDefault(e => e.Id == Id);
        }

        public async Task<List<MembershipRequests>> GetAllRequests(string UserId)
        {
            return await _Context.MembershipRequests.Include(e => e.RequestingUser).Where(e => e.UserId == UserId).ToListAsync();
                
        }

        public async Task<List<MembershipRequests>> GetAllRequests(int ClubId)
        {
            return await _Context.MembershipRequests.Include(e =>e.RequestingUser).Where(e => e.ClubId== ClubId).ToListAsync();

        }

        public async Task<MembershipRequests> GetRequestWithDoc(int Id)
        {
            return await _Context.MembershipRequests
                .Include(e =>  e.Documents).Include(e => e.RequestingUser).Include(e=>e.Branch)
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

    }
}
