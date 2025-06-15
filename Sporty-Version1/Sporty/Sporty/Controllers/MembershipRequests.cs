using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sporty.Enums;
using Sporty.Models;
using Sporty.Repositories;
using Sporty.Services;
using Sporty.ViewModels;
namespace Sporty.Controllers
{
    public class MembershipRequestsController : Controller
    {
        private readonly MembershipRequestService _membershipRequestService;
        private readonly BranchService _branchService;
        private readonly UserManager<User> _userManager;
        private readonly MembershipTypesService _membershipTypesService;
        public MembershipRequestsController(MembershipRequestService membershipRequestService, BranchService branchService, MembershipTypesService membershipTypesService, UserManager<User> userManager)
        {
            _membershipRequestService = membershipRequestService;
            _branchService = branchService;
            _membershipTypesService = membershipTypesService;
            _userManager = userManager;
        }


        public IActionResult Index(string Message)
        {
            return Ok(new { Message = Message ?? "Welcome to the Membership Requests Page!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetMembershipTypes(int branchId)
        {
            var types = await _membershipTypesService.GetAllTypes(branchId);
            return Json(types);
        }


        [HttpGet]
        public async Task<IActionResult> AddRequest(int ClubID)
        {
            var ViewModel = new MembershipRequestViewModel
            {
                ClubId = ClubID,
                Branches = await _branchService.GetAllBranches(ClubID),
            };
            ViewModel.UserId = _userManager.GetUserId(User);

            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(MembershipRequestViewModel model)
        {

            try
            {
                await _membershipRequestService.AddRequest(Request.Form);
            }

            catch (Exception ex)
            {

                TempData["Error"] = "Something went wrong" + ex.Message;
                // Refill dropdowns
                model.Branches = await _branchService.GetAllBranches(model.ClubId);
                model.MembershipTypes = await _membershipTypesService.GetAllTypes(model.BranchId);

                return View(model); // Return with previous values
            }

            // TempData["Success"] = "Membership request submitted successfully!";
            return RedirectToAction("DisplayRequests");

        }



        public async Task<IActionResult> DisplayRequests()
        {
            var requests=new List<MembershipRequests>();

            if (User.IsInRole("ClubAdmin"))
            {
                int clubId = _userManager.GetUserAsync(User).Result.ClubId.Value;
                 requests = await _membershipRequestService.GetRequestsByClubId(clubId);
                
            }
            else if(User.IsInRole("User"))
            {
                string UserId = _userManager.GetUserId(User);
                 requests = await _membershipRequestService.GetRequestsByUserId(UserId);
            }
                return View(requests);
        }

        [Authorize(Roles = "ClubAdmin")]
        public async Task<IActionResult> DisplayRequestsInDetails(MembershipRequestUpdateViewModel model)
        {
            var requestWithDoc = await _membershipRequestService.GetRequestWithDocuments(model.Id);
            if (requestWithDoc == null)
            {
                return NotFound("Request not found.");
            }
            ViewBag.Request = requestWithDoc;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ClubAdmin")]
        public async Task<IActionResult> TakeDecision(MembershipRequestUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _membershipRequestService.TakeDecision(model);
                return RedirectToAction(nameof(DisplayRequests));
            }

            var requestWithDoc = await _membershipRequestService.GetRequestWithDocuments(model.Id);
            if (requestWithDoc == null)
            {
                return NotFound("Request not found.");
            }
            ViewBag.Request = requestWithDoc;

            return View(nameof(DisplayRequestsInDetails),model);
        }

        // Reject a request by id

    }
}
