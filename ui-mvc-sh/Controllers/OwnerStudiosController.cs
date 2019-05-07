using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SS.DATA.EF;
using System.IO;
using System.Drawing;
using SS.UTILITIES;
using Microsoft.AspNet.Identity;

namespace SS.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin, Artist")]
    public class OwnerStudiosController : Controller
    {
        private StudioSearchEntities db = new StudioSearchEntities();

        public ActionResult MyStudios()
        {
            ViewBag.UserID = User.Identity.GetUserId();
            string artistID = User.Identity.GetUserId();
            var studioList = db.OwnerStudios.Where(o => o.OwnerID == artistID);
            return View(studioList);

        }
        // GET: OwnerStudios
        public ActionResult Index()
        {
            string artistID = User.Identity.GetUserId();
            int nbrRecords = db.ArtistAssets.Where(a => a.ArtistID == artistID).Count();
            if (nbrRecords == 0)
            {
                return RedirectToAction("Create", "ArtistAssets");

            }

            else
            {
                return View(db.OwnerStudios.ToList());
            }
        }

        // GET: OwnerStudios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerStudio ownerStudio = db.OwnerStudios.Find(id);
            if (ownerStudio == null)
            {
                return HttpNotFound();
            }
            return View(ownerStudio);
        }

        // GET: OwnerStudios/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: OwnerStudios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OwnerStudioID,StudioName,OwnerID,StudioPhoto,StudioAbout,ProducerAvailable,DateAdded,Address,City,State,ZipCode,ReservationLimit,StudioRules,MapLink")] OwnerStudio ownerStudio, HttpPostedFileBase StudioPhoto)
        {
            if (ModelState.IsValid)
            {
                
                ownerStudio.OwnerID = User.Identity.GetUserId();

                if (StudioPhoto != null)
                {
                    var savePath = Server.MapPath("~/Content/Images/OwnerStudios/");
                    var fileName = ownerStudio.StudioName.Replace(" ", string.Empty) + Path.GetExtension(StudioPhoto.FileName);

                    Image image = Image.FromStream(StudioPhoto.InputStream, true, true);
                    ownerStudio.StudioPhoto = fileName;

                    ImageUtilities.ResizeImage(savePath, fileName, image, 500, true, 50);
                    ownerStudio.StudioPhoto = fileName;

                }

                db.OwnerStudios.Add(ownerStudio);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(ownerStudio);
        }

        // GET: OwnerStudios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerStudio ownerStudio = db.OwnerStudios.Find(id);
            if (ownerStudio == null)
            {
                return HttpNotFound();
            }
            return View(ownerStudio);
        }

        // POST: OwnerStudios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OwnerStudioID,StudioName,OwnerID,StudioPhoto,StudioAbout,ProducerAvailable,DateAdded,Address,City,State,ZipCode,ReservationLimit,StudioRules,MapLink")] OwnerStudio ownerStudio, HttpPostedFileBase StudioPhoto, string OriginalPhoto)
        {
            if (ModelState.IsValid)
            {
                if (StudioPhoto != null)
                {
                    var savePath = Server.MapPath("~/Content/Images/OwnerStudios/");
                    var fileName = ownerStudio.StudioName.Replace(" ", string.Empty) + Path.GetExtension(StudioPhoto.FileName);
                    Image image = Image.FromStream(StudioPhoto.InputStream, true, true);
                    ownerStudio.StudioPhoto = fileName;
                }
                else
                {
                    ownerStudio.StudioPhoto = OriginalPhoto;
                }

                db.Entry(ownerStudio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ownerStudio);
        }

        // GET: OwnerStudios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerStudio ownerStudio = db.OwnerStudios.Find(id);
            if (ownerStudio == null)
            {
                return HttpNotFound();
            }
            return View(ownerStudio);
        }

        // POST: OwnerStudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OwnerStudio ownerStudio = db.OwnerStudios.Find(id);
            var savePath = Server.MapPath("~/Content/Images/OwnerStudios/");

            OwnerStudio studio = db.OwnerStudios.Find(id);
            ImageUtilities.Delete(savePath + ownerStudio.StudioPhoto);
            ImageUtilities.Delete(savePath + "t_" + ownerStudio.StudioPhoto);

            db.OwnerStudios.Remove(ownerStudio);
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
