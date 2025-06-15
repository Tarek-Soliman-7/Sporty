using Sporty.ViewModels;
using Sporty.Repositories;
using Sporty.Enums;
using Sporty.Models;
using Sporty.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Sporty.Repositories.Interfaces;

namespace Sporty.Controllers
{
    [Authorize(Roles = "ClubAdmin,User")]
    public class EventController : Controller
    {

        private readonly IBranchRepository _branchRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IEventRepo _eventRepo;
        private readonly UserManager<User> _userManager;

        public EventController(IBranchRepository branchRepository, IClubRepository clubRepository, UserManager<User> userManager, IEventRepo eventRepo)
        {
            _branchRepository = branchRepository;
            _clubRepository = clubRepository;
            _eventRepo=eventRepo;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? clubId)
        {
            ViewData["clubs"] = await _clubRepository.GetAllClubsAsync();
            if (clubId is not null)
            {
                var Events = await _eventRepo.SearchByIdClubAsync(clubId.Value);
                return View(Events);
            }
            else
            {
                var Events = await _eventRepo.GetAllAsync();
                
                return View(Events);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["branches"] =await _branchRepository.GetAllBranchesAsync();
            ViewData["clubs"] =await  _clubRepository.GetAllClubsAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["branches"] =await _branchRepository.GetAllBranchesAsync();
                ViewData["clubs"] =await _clubRepository.GetAllClubsAsync();
                return BadRequest("Invalid_Inpute");
            }
            if(model.Date<=DateTime.Today) return BadRequest("Invalid_Inpute");
            if (model.Image is not null)
            {
                model.ImageName=DocmentManage.Upload(model.Image);
            }
            var even = new Event()
            {
                BranchId = model.BranchId,
                ClubId= model.ClubId,
                Description = model.Description,
                Title = model.Title,
                Date = model.Date,
                ImageName = model.ImageName,
            };
            await _eventRepo.AddAsync(even);
            var count =  _eventRepo.Save();
            if (count > 0)
            {
                return RedirectToAction("Index");
            }
            ViewData["branches"] =await _branchRepository.GetAllBranchesAsync();
            ViewData["clubs"] =await _clubRepository.GetAllClubsAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetBranchesByClub(int clubId)
        {
            var branches = await _branchRepository.GetAllBranchesAsync();
            var filteredBranches = branches
                .Where(b => b.ClubId == clubId)
                .Select(b => new { b.Id, b.Name })
                .ToList();

            return Json(filteredBranches);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(!id.HasValue)return BadRequest();
            else
            {
                var even=await _eventRepo.GetByIdAsync(id.Value);
                if (even == null)
                {
                    return NotFound();
                }
                else
                {
                    if (even.ImageName is not null) DocmentManage.Delete(even.ImageName);
                    _eventRepo.Delete(even);
                    var count = _eventRepo.Save();
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

        [HttpGet]

        public async Task<IActionResult> Details(int? id)
        {
            ViewData["branches"] = await _branchRepository.GetAllBranchesAsync();
            ViewData["clubs"] = await _clubRepository.GetAllClubsAsync();
            if (!id.HasValue)return BadRequest();
            var even =await _eventRepo.GetByIdAsync(id.Value);
            var ev = new EventViewModel()
            {
                Date = even.Date,
                Description = even.Description,
                Title = even.Title,
                ImageName = even.ImageName,
                BranchId = even.BranchId,
                ClubId = even.ClubId,
                id=even.Id
            };
            return View(ev);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int? id)
        {
            ViewData["branches"] = await _branchRepository.GetAllBranchesAsync();
            ViewData["clubs"] = await _clubRepository.GetAllClubsAsync();
            if (!id.HasValue) return BadRequest();
            var evn = await _eventRepo.GetByIdAsync(id.Value);
            if (evn is null) return NotFound();
            var even = new EventViewModel()
            {
                BranchId = evn.BranchId,
                ClubId = evn.ClubId,
                ImageName = evn.ImageName,
                Title = evn.Title,
                Description = evn.Description,
                Date = evn.Date,
                id=evn.Id
            };
            return View(even);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromRoute]int id ,EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["branches"] = await _branchRepository.GetAllBranchesAsync();
                ViewData["clubs"] = await _clubRepository.GetAllClubsAsync();
                return BadRequest("Invalid_Inpute");
            }
            if (model.Date <= DateTime.Today) return BadRequest("Invalid_Inpute");

            if(model.Image != null)
    {
            
                if (!string.IsNullOrEmpty(model.ImageName))
                {
                    DocmentManage.Delete(model.ImageName);
                }

                model.ImageName = DocmentManage.Upload(model.Image);
            }

            var even = new Event()
            {
                Id=model.id,
                BranchId = model.BranchId,
                ClubId = model.ClubId,
                Description = model.Description,
                Title = model.Title,
                Date = model.Date,
                ImageName = model.ImageName,
            };
             _eventRepo.Update(even);
            var count = _eventRepo.Save();
            if (count > 0)
            {
                return RedirectToAction("Index");
            }
            ViewData["branches"] = await _branchRepository.GetAllBranchesAsync();
            ViewData["clubs"] = await _clubRepository.GetAllClubsAsync();
            return View(model);
        }
        
    }



    
}
