using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Sporty.Models;
namespace Sporty.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Membership> memberships { get; set; }
        public DbSet<MembershipRequests> MembershipRequests { get; set; }
        public DbSet<MembershipTypes> MembershipTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payment { get; set; }

        public DbSet<ClubWallet> ClubWallets { get; set; }
        public DbSet<Payout> Payout { get; set; }



    }

}