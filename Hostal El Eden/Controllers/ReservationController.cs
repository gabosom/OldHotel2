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
    public class ReservationController : Controller
    {
        private HotelContext db = new HotelContext();

        //
        // GET: /ReservationAside
        [ChildActionOnly]
        public PartialViewResult ReservationAside()
        {
            return PartialView("ReservationAside");
        }

        //
        // GET: /Reservation/

        public ViewResult Index()
        {
            return View("Search", "_NonHomePage", db.Categories.ToList());
        }

        //
        // GET: /Reservation/Details/5

        public ViewResult Details(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            return View(reservation);
        }

        //
        // GET: /Reservation/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Reservation/Create

        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(reservation);
        }
        
        //
        // GET: /Reservation/Edit/5
 
        public ActionResult Edit(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            return View(reservation);
        }

        //
        // POST: /Reservation/Edit/5

        [HttpPost]
        public ActionResult Edit(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        //
        // GET: /Reservation/Delete/5
 
        public ActionResult Delete(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            return View(reservation);
        }

        //
        // POST: /Reservation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /*public ViewResult Search(DateTime ResCheckinDate, DateTime ResCheckoutDate, int ResNumOfGuests)*/
        public ViewResult Search(ReservationSearch ResSearch)
        {
            var OccupiedRoomsIds = from res in db.Reservations
                                   where ((res.ResCheckInDate >= ResSearch.ResCheckinDate && res.ResCheckInDate <= ResSearch.ResCheckoutDate) || (res.ResCheckInDate <= ResSearch.ResCheckinDate && res.ResCheckOutDate >= ResSearch.ResCheckoutDate) || (res.ResCheckInDate <= ResSearch.ResCheckinDate && res.ResCheckOutDate >= ResSearch.ResCheckinDate))
                                   select res.Room;

            var AvailableCategories = (from room in (db.Rooms.Except(OccupiedRoomsIds).Include("Category").ToList())
                                                 select room.Category).Distinct();


            ViewBag.ResSearch = ResSearch;
            return View(AvailableCategories);
        }
    }
}