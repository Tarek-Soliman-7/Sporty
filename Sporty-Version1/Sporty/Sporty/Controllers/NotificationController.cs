using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sporty.Models;
using Sporty.Services;
using Sporty.Services.Interfaces;
using Sporty.ViewModels;

namespace Sporty.Controllers
{
    public class NotificationController : Controller
    {
        private INotificationService _notificationService;
        private readonly UserManager<User> _userManager;
        public NotificationController(INotificationService notificationService,UserManager<User>userManager)
        {
            _notificationService = notificationService;
            _userManager = userManager;
        }

        private string GetSignedUseridAsync()
        {
           return _userManager.GetUserId(User);
           
        }

        public async Task<IActionResult> GetUnRead(){

            List<Notification>notifications = await _notificationService.GetUnreadNotificationsAsync(GetSignedUseridAsync());
            return View("GetNotifications",notifications);

        }

        public async Task<IActionResult> GetRead()
        {
            List<Notification> notifications = await _notificationService.GetReadNotificationAsync(GetSignedUseridAsync());
            return View("GetNotifications", notifications);

        }

        public async Task<IActionResult> AddNotification(NotificationViewModel notification)
        {
           await  _notificationService.AddNotificationAsync(notification);
            return Ok();
        }
        
    }
}
