using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sporty.Models;
using Sporty.Services.Interfaces;
using Sporty.Services;
using Sporty.ViewModels;
using Sporty.Repositories.Interfaces;
namespace Sporty.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IMembershipService _service;
        private readonly IClubService _IClubService;
        public MembershipController(IMembershipService service,IClubService IClubService)
        {
            _service = service;
            _IClubService = IClubService;
        }

        // GET: /Membership
        public async Task<IActionResult> Index()
        {
            var memberships = await _service.GetAllMembershipsAsync();
            return View(memberships);
        }

        // GET: /Membership/Create
        public IActionResult Create()
        {
            var list = _IClubService.GetAllClubsAsync().Result;
            return View(list);
        }

        // POST: /Membership/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Membership model)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateMembershipAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
