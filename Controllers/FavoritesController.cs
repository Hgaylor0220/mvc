using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using mvc.Data;

namespace mvc.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FavoritesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Favorite> model = _db.Favorites.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Favorite favorite)
        {
            _db.Favorites.Add(favorite);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisFavorite = _db.Favorites
                .Include(favorite => favorite.Rivers)
                .ThenInclude(join => join.River)
                .FirstOrDefault(favorite => favorite.FavoriteId == id);
            return View(thisFavorite);
        }

        public ActionResult Edit(int id)
        {
            var thisFavorite = _db.Favorites.FirstOrDefault(favorite => favorite.FavoriteId == id);
            return View(thisFavorite);
        }

        [HttpPost]
        public ActionResult Edit(Favorite favorite)
        {
            _db.Entry(favorite).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisFavorite = _db.Favorites.FirstOrDefault(favorite => favorite.FavoriteId == id);
            return View(thisFavorite);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisFavorite = _db.Favorites.FirstOrDefault(favorite => favorite.FavoriteId == id);
            _db.Favorites.Remove(thisFavorite);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}