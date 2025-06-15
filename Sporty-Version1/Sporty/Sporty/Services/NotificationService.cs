using Sporty.Models;
using Sporty.Repositories.Interfaces;
using Sporty.Services.Interfaces;
using Sporty.ViewModels;
namespace Sporty.Services
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task AddNotificationAsync(NotificationViewModel notificationView)
        {
            var notification = new Notification
            {
                UserId = notificationView.UserId,
                Title = notificationView.Title,
                Message = notificationView.Message,
                IsRead = false,
                CreatedAt = DateTime.Now
            };
            await _repository.AddAsync(notification);
        }

        public Task<List<Notification>> GetUnreadNotificationsAsync(string userId) => _repository.GetNotificationsByUserAsync(userId,false);

        public Task<List<Notification>> GetReadNotificationAsync(string userId) => _repository.GetNotificationsByUserAsync(userId,true);

        public Task MarkAsReadAsync(string notificationId) =>  _repository.MarkAsReadAsync(notificationId);
    }
}
