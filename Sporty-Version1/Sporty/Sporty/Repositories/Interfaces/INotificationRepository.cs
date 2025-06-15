using Sporty.Models;

namespace Sporty.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<List<Notification>> GetNotificationsByUserAsync(string userId, bool? isRead = null);
        Task MarkAsReadAsync(string id);
    }
}
