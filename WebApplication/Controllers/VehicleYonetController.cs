﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using System.IO;

namespace WebApplication.Controllers
{
    public class VehicleYonetController : Controller
    {
        private Db_AracKiralamaEntities db = new Db_AracKiralamaEntities();

        // GET: VehicleYonet
        public ActionResult Index()
        {
            return View(db.Tbl_Araclar.ToList());
        }

        // GET: VehicleYonet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Araclar tbl_Araclar = db.Tbl_Araclar.Find(id);
            if (tbl_Araclar == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Araclar);
        }

        // GET: VehicleYonet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleYonet/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AracId,Marka,Model,ModelYili,Yakit,Vites,Fiyat")] Tbl_Araclar tbl_Araclar)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Araclar.Add(tbl_Araclar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Araclar);
        }

        // GET: VehicleYonet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Araclar tbl_Araclar = db.Tbl_Araclar.Find(id);
            if (tbl_Araclar == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Araclar);
        }

        // POST: VehicleYonet/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AracId,Marka,Model,ModelYili,Yakit,Vites,Fiyat")] Tbl_Araclar tbl_Araclar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Araclar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Araclar);
        }

        // GET: VehicleYonet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Araclar tbl_Araclar = db.Tbl_Araclar.Find(id);
            if (tbl_Araclar == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Araclar);
        }
        // POST: VehicleYonet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Araclar tbl_Araclar = db.Tbl_Araclar.Find(id);
            db.Tbl_Araclar.Remove(tbl_Araclar);
            db.SaveChanges();

            string ImageFileName = id.ToString() + ".jpg";
            string FolderPath = Path.Combine(Server.MapPath("~/VehicleImages"), ImageFileName);
            if (System.IO.File.Exists(FolderPath))
            {
                System.IO.File.Delete(FolderPath);
            }
            // Resim burada silinmiş oluyor
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SaveImages()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveImages(string hiddenId,HttpPostedFileBase UploadedImage)
        {
            if (UploadedImage.ContentLength > 0) {
                string ImageFileName = hiddenId + ".jpg";
                string FolderPath = Path.Combine(Server.MapPath("~/VehicleImages"),ImageFileName);
                UploadedImage.SaveAs(FolderPath);
            }
            ViewBag.Message = hiddenId + ".jpg isimli Resim başarıyla yüklendi.";

            return View();
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
