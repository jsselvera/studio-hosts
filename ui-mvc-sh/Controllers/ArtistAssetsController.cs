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
    public class ArtistAssetsController : Controller
    {
        private StudioSearchEntities db = new StudioSearchEntities();



        // GET: ArtistAssets
        public ActionResult Index()
        {
            ViewBag.UserID = User.Identity.GetUserId();
            string artistID = User.Identity.GetUserId();
            int nbrRecords = db.ArtistAssets.Where(a => a.ArtistID == artistID).Count();

            if(nbrRecords == 0)
            {
                return RedirectToAction("Create", "ArtistAssets");
                
            }

            else
            {
            var artistAssets = db.ArtistAssets.Include(a => a.ArtistDetail);
            return View(artistAssets.ToList());
            }
        }
        
        // GET: ArtistAssets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistAsset artistAsset = db.ArtistAssets.Find(id);
            if (artistAsset == null)
            {
                return HttpNotFound();
            }
            return View(artistAsset);
        }

        

        // GET: ArtistAssets/Create
        public ActionResult Create()
        {
            ViewBag.ArtistID = new SelectList(db.ArtistDetails, "ArtistID", "FirstName");
            return View();
        }

        // POST: ArtistAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistAssetID,ArtistName,ArtistGenre,ArtistPhoto,ArtistBio,ArtistID,ArtistLink")] ArtistAsset artistAsset, HttpPostedFileBase ArtistPhoto)
        {
            if (ModelState.IsValid)
            {
                artistAsset.ArtistID = User.Identity.GetUserId();

                if (ArtistPhoto != null)
                {
                    var savePath = Server.MapPath("~/Content/Images/ArtistAssets/");
                    var fileName = artistAsset.ArtistName + Path.GetExtension(ArtistPhoto.FileName);
                    Image image = Image.FromStream(ArtistPhoto.InputStream, true, true);

                    ImageUtilities.ResizeImage(savePath, fileName, image, 500, true, 50);

                    artistAsset.ArtistPhoto = fileName;
                }

                db.ArtistAssets.Add(artistAsset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.ArtistDetails, "ArtistID", "FirstName", artistAsset.ArtistID);
            return View(artistAsset);
        }

        // GET: ArtistAssets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistAsset artistAsset = db.ArtistAssets.Find(id);
            if (artistAsset == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistID = new SelectList(db.ArtistDetails, "ArtistID", "FirstName", artistAsset.ArtistID);
            return View(artistAsset);
        }

        // POST: ArtistAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistAssetID,ArtistName,ArtistGenre,ArtistPhoto,ArtistBio,ArtistID,ArtistLink")] ArtistAsset artistAsset, HttpPostedFileBase ArtistPhoto, string OriginalImage)
        {
            if (ModelState.IsValid)
            {
                if (ArtistPhoto != null)
                {
                    var savePath = Server.MapPath("~/Content/Images/ArtistAssets/");
                    var fileName = artistAsset.ArtistName + Path.GetExtension(ArtistPhoto.FileName);
                    Image image = Image.FromStream(ArtistPhoto.InputStream, true, true);

                    ImageUtilities.ResizeImage(savePath, fileName, image, 500, true, 50);

                    artistAsset.ArtistPhoto = fileName;
                }
                else
                {
                    artistAsset.ArtistPhoto = OriginalImage;
                }


                    db.Entry(artistAsset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(db.ArtistDetails, "ArtistID", "FirstName", artistAsset.ArtistID);
            return View(artistAsset);
        }

        // GET: ArtistAssets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistAsset artistAsset = db.ArtistAssets.Find(id);
            if (artistAsset == null)
            {
                return HttpNotFound();
            }
            return View(artistAsset);
        }

        // POST: ArtistAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var savePath = Server.MapPath("~/Content/Images/ArtistAssets/");

            ArtistAsset artistAsset = db.ArtistAssets.Find(id);
            ImageUtilities.Delete(savePath + artistAsset.ArtistPhoto);
            ImageUtilities.Delete(savePath + "t_" + artistAsset.ArtistPhoto);

            db.ArtistAssets.Remove(artistAsset);
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

        //GET: Profile
        public ActionResult Profile(string UserID)
        {
            ViewBag.UserID = User.Identity.GetUserId();
            string artistID = User.Identity.GetUserId();
            int nbrRecords = db.ArtistAssets.Where(a => a.ArtistID == artistID).Count();

            if (nbrRecords == 0)
            {
                return RedirectToAction("Create", "ArtistAssets");

            }

            else
            {
                ArtistAsset artistProfile = db.ArtistAssets.Where(a => a.ArtistID == artistID).SingleOrDefault();
                return View("Profile", artistProfile);
            }
           
        }

    }
}
