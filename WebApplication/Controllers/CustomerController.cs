using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CustomerController : Controller
    {
        Db_AracKiralamaEntities db = new Db_AracKiralamaEntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public static string GetMD5_2(string str)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] fromData = Encoding.UTF8.GetBytes(str);
                byte[] targetData = md5.ComputeHash(fromData);
                StringBuilder byte2String = new StringBuilder();
                for (int i = 0; i < targetData.Length; i++)
                {
                    byte2String.Append(targetData[i].ToString("x2"));
                }
                return byte2String.ToString(); // ToString yok 
            }
        }

        [HttpPost]
        public ActionResult Giris(FormCollection bilgi)
        {
            string tc = bilgi["tc"].ToString();
            string sif = GetMD5_2(bilgi["sif"].ToString());
            var count = db.Tbl_Musteriler.Where(x => x.TcKimlik == tc && x.Sifre == sif).Count();

            if (count == 0)
            {
                ViewData["sonuc"] = "HATA! Kayıtlı değilsiniz veya bilgileriniz yanlış.";
            }
            else
            {
                Session["session_giris"] = true;
                Session["session_tc"] = tc;
                ViewData["sonuc"] = "Tebrikler...";
                //return RedirectToAction("Profil", "Customer");
            }
            return View();
        }

    }
}