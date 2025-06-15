using Microsoft.EntityFrameworkCore;
using Sporty.Data;
using Sporty.Models;
using Sporty.Repositories.Interfaces;
namespace Sporty.Repositories
{
    public class NotificationRepository:INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetNotificationsByUserAsync(string userId, bool? isRead = null)
        {
            var query = _context.Notifications.Where(n => n.UserId == userId);

            if (isRead.HasValue)
                query = query.Where(n => n.IsRead == isRead.Value);

            return await query.OrderByDescending(n => n.CreatedAt).ToListAsync();

        }

        public async Task MarkAsReadAsync(string id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
