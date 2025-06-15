using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sporty.Models;
using Sporty.Repositories.Interfaces;
using Sporty.Services;
using Sporty.ViewModels;
using System.Threading.Tasks;

namespace Sporty.Controllers
{
    public class BranchController : Controller
    {
       
        private readonly BranchService _branchService;
        private readonly UserManager<User> _userManager;

        public BranchController(BranchService branchService,UserManager<User>userManager)
        {
            _branchService = branchService;
            _userManager = userManager;
        }


        private async Task<int?> GetClubIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.ClubId;
        }
        
        public async Task<IActionResult> IndexById(int Id)
        {
            var branches = await _branchService.GetAllBranches(Id);
            return View("Index",branches);
        }

        // GET: Branch
       
        public async Task<IActionResult> Index()
        {
            var ClubId= await GetClubIdAsync();
            var branches = await _branchService.GetAllBranches(ClubId.Value);
            return View(branches);
        }

        // GET: Branch/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _branchService.GetBranchWithClubID(id.Value);
            
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // GET: Branch/Create
        [Authorize(Roles = "ClubAdmin")]
        public async Task<IActionResult> Create()
        {
    
            return View();
        }

        // POST: Branch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ClubAdmin")]
        public async Task<IActionResult> Create(BranchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var clubId= await GetClubIdAsync();
                model.ClubId=clubId.Value;
                _branchService.AddBranch(model);
                return RedirectToAction(nameof(Index));
            }
            
            
            return View(model);
        }

        // GET: Branch/Edit/5
        [Authorize(Roles = "ClubAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _branchService.GetBranch(id.Value);
            var clubId = await GetClubIdAsync();

            if (branch == null)
            {
                return NotFound();
            }
            else if (branch.ClubId != clubId) { return Unauthorized(); }

                var viewModel = new BranchViewModel
                {
                    Id = branch.Id,
                    Name = branch.Name,
                    Location = branch.Location,
                    PhoneNumber = branch.PhoneNumber,
                    Email = branch.Email,
                    
                };

            
            return View(viewModel);
        }

        // POST: Branch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ClubAdmin")]
        public async Task<IActionResult> Edit(int id, BranchViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    var branch = await _branchService.GetBranch(id);
                    var clubId = await GetClubIdAsync();

                    if (branch == null)
                    {
                        return NotFound();
                    }
                    else if (branch.ClubId != clubId) { return Unauthorized(); }

                    branch.Name = model.Name;
                    branch.Location = model.Location;
                    branch.PhoneNumber = model.PhoneNumber;
                    branch.Email = model.Email;

                    await _branchService.UpdateBranch(branch);
                
                return RedirectToAction(nameof(Index));
            }
            
            
            return View(model);
        }

        // GET: Branch/Delete/5
        [Authorize(Roles = "ClubAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _branchService.GetBranch(id.Value);
            var clubId = await GetClubIdAsync();

            if (branch == null)
            {
                return NotFound();
            }
            else if (branch.ClubId != clubId) { return Unauthorized(); }


            return View(branch);
        }

        // POST: Branch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ClubAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _branchService.DeleteBranch(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
