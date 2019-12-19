using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using mvc.Data;

namespace mvc.Controllers
{
    [Authorize]
    public class RiversController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public RiversController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var userRivers = _db.River.Where(entry => entry.User.Id == currentUser.Id);
            return View(userRivers);
        }

        public ActionResult Create()
        {
            ViewBag.FavoriteId = new SelectList(_db.Favorites, "FavoriteId", "FavoriteCategory");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(River river, int FavoriteId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            river.User = currentUser;
            _db.River.Add(river);
            if (FavoriteId != 0)
            {
                _db.FavoriteRiver.Add(new FavoriteRiver() { FavoriteId = FavoriteId, RiverId = river.RiverId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisRiver = _db.River
                .Include(river => river.Favorites)
                .ThenInclude(join => join.Favorite)
                .FirstOrDefault(river => river.RiverId == id);
            return View(thisRiver);
        }

        public ActionResult Edit(int id)
        {
            var thisRiver = _db.River.FirstOrDefault(rivers => rivers.RiverId == id);
            ViewBag.FavoriteId = new SelectList(_db.Favorites, "FavoriteId", "FavoriteCategory");
            return View(thisRiver);
        }

        [HttpPost]
        public ActionResult Edit(River river, int FavoriteId)
        {
            if (FavoriteId != 0)
            {
                _db.FavoriteRiver.Add(new FavoriteRiver() { FavoriteId = FavoriteId, RiverId = river.RiverId });
            }
            _db.Entry(river).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddFavorite(int id)
        {
            var thisRiver = _db.River.FirstOrDefault(rivers => rivers.RiverId == id);
            ViewBag.FavoriteId = new SelectList(_db.Favorites, "FavoriteId", "FavoriteCategory");
            return View(thisRiver);
        }

        [HttpPost]
        public ActionResult AddFavorite(River river, int FavoriteId)
        {
            if (FavoriteId != 0)
            {
                _db.FavoriteRiver.Add(new FavoriteRiver() { FavoriteId = FavoriteId, RiverId = river.RiverId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var thisRiver = _db.River.FirstOrDefault(rivers => rivers.RiverId == id);
            return View(thisRiver);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisRiver = _db.River.FirstOrDefault(rivers => rivers.RiverId == id);
            _db.River.Remove(thisRiver);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteFavorite(int joinId)
        {
            var joinEntry = _db.FavoriteRiver.FirstOrDefault(entry => entry.FavoriteRiverId == joinId);
            _db.FavoriteRiver.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult> SortByRating()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var userList = _db.River.Where(entry => entry.User.Id == currentUser.Id);
            return View("Index");
        }
    }
}