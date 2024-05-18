using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class RezervationController : Controller
    {
        Db_AracKiralamaEntities db = new Db_AracKiralamaEntities();
        // GET: Rezervation
        public ActionResult Index()
        {
            if (Session["session_giris"] != null)
            {
                string tc = Session["session_tc"].ToString();
                return View(db.Tbl_Rezervasyonlar.Where(x=>x.TcKimlik==tc).ToList());
            }
            else
            {
                return RedirectToAction("Giris","Customer");
            }
            
        }
    }
}