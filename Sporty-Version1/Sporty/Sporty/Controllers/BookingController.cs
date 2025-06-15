using Sporty.Enums;
using Sporty.Models;
using App.PR.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sporty.Enums;
using Sporty.Models;
using Sporty.Repositories.Interfaces;
using Sporty.ViewModels;
using System.Security.Claims;

namespace App.PR.Controllers
{

    public class BookingController : Controller
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IEventRepo _eventRepo;
        private readonly IBookingRepo _bookingRepo;
        private readonly UserManager<User> _userManager;

        public BookingController(IBranchRepository branchRepository, IClubRepository clubRepository, UserManager<User> userManager, IEventRepo eventRepo,IBookingRepo bookingRepo)
        {
            _branchRepository = branchRepository;
            _clubRepository = clubRepository;
            _eventRepo = eventRepo;
            _userManager = userManager;
            _bookingRepo = bookingRepo;
        }
        public async Task<IActionResult> Book(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var model = await _eventRepo.GetByIdAsync(id);

            // Add null check for the retrieved model
            if (model == null)
            {
                return NotFound(); // Or handle appropriately
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(); 
            }
            var book = new  Booking()
            {
                Date = model.Date,
                EventId = model.Id,
                UserId = userId,
                status= BookingStatus.Confirmed,
            };

             await _bookingRepo.AddAsync(book);
            
            var count =  _bookingRepo.Save(); 
            if (count > 0)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? clubId)
        {
            ViewData["clubs"] = await _clubRepository.GetAllClubsAsync();
            if (clubId is not null)
            {
                var Events = await _bookingRepo.SearchByIdClubAsync(clubId.Value);
                return View(Events);
            }
            else
            {
                var Events = await _bookingRepo.GetAllAsync();

                return View(Events);
            }

            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            else
            {
                var even = await _bookingRepo.GetByIdAsync(id.Value);
                if (even == null)
                {
                    return NotFound();
                }
                else
                {
                       _bookingRepo.Delete(even);
                    var count = _bookingRepo.Save();
                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }

        }

    }
}
