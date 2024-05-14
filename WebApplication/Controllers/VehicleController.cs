using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class VehicleController : Controller
    {
        Db_AracKiralamaEntities db = new Db_AracKiralamaEntities();
        // GET: Vehicle
        public ActionResult Index()
        {
            return View(db.Tbl_Araclar.ToList());
        }
        public ActionResult Details(int? id)
        {
            Tbl_Araclar arac_bilgileri = db.Tbl_Araclar.Find(id);
            return View(arac_bilgileri);
        }
    }
}