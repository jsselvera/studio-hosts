using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SS.DATA.EF;
using Microsoft.AspNet.Identity;

namespace SS.UI.MVC.Controllers
{
    public class ReservationsController : Controller
    {
        private StudioSearchEntities db = new StudioSearchEntities();

        public ActionResult MyReservations()
        {
            string userid = User.Identity.GetUserId();

            //string artistID = User.Identity.GetUserId();
            //var reservationList = db.Reservations.Where(r => r.ArtistAsset.ArtistID == artistID);
            //return View (reservationList);

            List<Reservation> myReservations = new List<Reservation>();

            List<OwnerStudio> studios = db.OwnerStudios.Where(s => s.OwnerID == userid).ToList();

                List<ArtistAsset> aa = db.ArtistAssets.Where(a => a.ArtistID == userid).ToList();

                foreach (var s in studios)
            {
                foreach (var r in db.Reservations)
                {
                    if (r.OwnerStudioID == s.OwnerStudioID)
                    {
                        myReservations.Add(r);
                    }
                }
            }

            foreach (var a in aa)
            {
                foreach (var r in db.Reservations)
                {
                    if (r.ArtistAssetID == a.ArtistAssetID)
                    {
                        myReservations.Add(r);
                    }
                }
            }

            return View(myReservations);
        }

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.ArtistAsset).Include(r => r.OwnerStudio);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create(int? id)
        {
            ViewBag.OwnerStudioID = new SelectList(db.OwnerStudios, "OwnerStudioID", "StudioName", id);
            return View();
            
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationID,OwnerStudioID,ReservationDate,ArtistAssetID,ReservationTime")] Reservation reservation)
        {

            if (ModelState.IsValid)
            {
                DateTime resDate = reservation.ReservationDate;
                resDate = reservation.ReservationDate;

                TimeSpan resTime = reservation.ReservationTime;
                resTime = reservation.ReservationTime;

                OwnerStudio resStudio = db.OwnerStudios.Where(m => m.OwnerStudioID == reservation.OwnerStudioID).Single();

                var userid = User.Identity.GetUserId();
            reservation.ArtistAssetID = db.ArtistAssets.Where(a => a.ArtistID == userid).SingleOrDefault().ArtistAssetID;


                if (db.Reservations.Where(m => m.ReservationDate == resDate).Count() < resStudio.ReservationLimit)
                {
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return RedirectToAction("MyReservations");
                }

                if (User.IsInRole("Admin"))
                {
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return RedirectToAction("MyReservations");
                }

                else
                {
                    
                    return RedirectToAction("ResLimitReach");
                }

            }

            
            ViewBag.OwnerStudioID = new SelectList(db.OwnerStudios, "OwnerStudioID", "StudioName", reservation.OwnerStudioID);
            

            
            return View(reservation);
        }

        public ActionResult ResLimitReach()
        {
            return View();
        }

        //GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistAssetID = new SelectList(db.ArtistAssets, "ArtistAssetID", "ArtistName", reservation.ArtistAssetID);
            ViewBag.OwnerStudioID = new SelectList(db.OwnerStudios, "OwnerStudioID", "StudioName", reservation.OwnerStudioID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,ReservationDate,OwnerStudioID,ArtistAssetID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistAssetID = new SelectList(db.ArtistAssets, "ArtistAssetID", "ArtistName", reservation.ArtistAssetID);
            ViewBag.OwnerStudioID = new SelectList(db.OwnerStudios, "OwnerStudioID", "StudioName", reservation.OwnerStudioID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("MyReservations");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
