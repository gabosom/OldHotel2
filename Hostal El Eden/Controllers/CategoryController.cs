using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hostal_El_Eden.Models;

namespace Hostal_El_Eden.Controllers
{ 
    public class CategoryController : Controller
    {
        private HotelContext db = new HotelContext();

        //
        // GET: /Category/

        public ViewResult Index()
        {
            ViewBag.ComesFromSearch = true;
            return View("Search", db.Categories.ToList());
        }

        //
        // GET: /Category/Details/5

        public ViewResult Details(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(category);
        }
        
        //
        // GET: /Category/Edit/5
 
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5
 
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public ViewResult SearchAvailability(DateTime ResCheckinDate, DateTime ResCheckoutDate, int ResNumOfGuests)
        {
            var OccupiedRoomsIds = from res in db.Reservations
                                   where ((res.ResCheckInDate >= ResCheckinDate && res.ResCheckInDate <= ResCheckoutDate) || (res.ResCheckInDate <= ResCheckinDate && res.ResCheckOutDate >= ResCheckoutDate) || (res.ResCheckInDate <= ResCheckinDate && res.ResCheckOutDate >= ResCheckinDate))
                                   select res.Room;

            var AvailableCategories = (from room in (db.Rooms.Except(OccupiedRoomsIds).Include("Category").ToList())
                                       select room.Category).Distinct();



            return View("Search", AvailableCategories);
        }
    }
}