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
    public class AdminRoomController : Controller
    {
        private HotelContext db = new HotelContext();

        //
        // GET: /AdminRoom/

        public ViewResult Index()
        {
            return View(db.Rooms.ToList());
        }

        //
        // GET: /AdminRoom/Details/5

        public ViewResult Details(int id)
        {
            Room room = db.Rooms.Find(id);
            return View(room);
        }

        //
        // GET: /AdminRoom/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AdminRoom/Create

        [HttpPost]
        public ActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(room);
        }
        
        //
        // GET: /AdminRoom/Edit/5
 
        public ActionResult Edit(int id)
        {
            Room room = db.Rooms.Find(id);
            return View(room);
        }

        //
        // POST: /AdminRoom/Edit/5

        [HttpPost]
        public ActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        //
        // GET: /AdminRoom/Delete/5
 
        public ActionResult Delete(int id)
        {
            Room room = db.Rooms.Find(id);
            return View(room);
        }

        //
        // POST: /AdminRoom/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}