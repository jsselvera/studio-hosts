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
    public class ArtistDetailsController : Controller
    {
        private StudioSearchEntities db = new StudioSearchEntities();

        // GET: ArtistDetails
        public ActionResult Index()
        {
            return View(db.ArtistDetails.ToList());
        }

        // GET: ArtistDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistDetail artistDetail = db.ArtistDetails.Find(id);
            if (artistDetail == null)
            {
                return HttpNotFound();
            }
            return View(artistDetail);
        }

        // GET: ArtistDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtistDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistID,FirstName,LastName")] ArtistDetail artistDetail)
        {
            if (ModelState.IsValid)
            {
                db.ArtistDetails.Add(artistDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artistDetail);
        }

        // GET: ArtistDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistDetail artistDetail = db.ArtistDetails.Find(id);
            if (artistDetail == null)
            {
                return HttpNotFound();
            }
            return View(artistDetail);
        }

        // POST: ArtistDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistID,FirstName,LastName")] ArtistDetail artistDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artistDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artistDetail);
        }

        // GET: ArtistDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistDetail artistDetail = db.ArtistDetails.Find(id);
            if (artistDetail == null)
            {
                return HttpNotFound();
            }
            return View(artistDetail);
        }

        // POST: ArtistDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ArtistDetail artistDetail = db.ArtistDetails.Find(id);
            db.ArtistDetails.Remove(artistDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
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
