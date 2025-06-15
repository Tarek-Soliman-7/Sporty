using Sporty.Models;
using Sporty.Repositories.Interfaces;
using Sporty.ViewModels;

namespace Sporty.Services.Interfaces
{
    public interface INotificationService
    {
         Task AddNotificationAsync(NotificationViewModel notificationView);
     
        Task<List<Notification>> GetUnreadNotificationsAsync(string userId);

        Task<List<Notification>> GetReadNotificationAsync(string userId);

        Task MarkAsReadAsync(string notificationId);
    }
}
