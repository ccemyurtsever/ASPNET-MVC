using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using System.Data.Entity;

namespace WebApplication.Controllers
{
    public class CustomerController : Controller
    {
        Db_AracKiralamaEntities db = new Db_AracKiralamaEntities();
        private bool x;

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

        public ActionResult Giris()
        {
            return View();
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
                // ViewData["sonuc"] = "Tebrikler...";
                return RedirectToAction("Profil", "Customer");
            }
            return View();

        }
        public ActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kayit([Bind(Include = "TcKimlik,AdSoyad,DogumTarihi,Cinsiyet,Sifre")] Tbl_Musteriler müsteri)
        {
            db.Tbl_Musteriler.Add(müsteri);
            int result = db.SaveChanges();
            if (result > 0)
            {
                ViewData["sonuc"] = "Tebrikler ! Kaydınız Gerçekleşti ...";
            }
            else
            {
                ViewData["sonuc"] = "Hata ! Kaydınız Gerçekleştirilemedi ...";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Profil()
        {
            if (Session["session_giris"] != null)
            {
                string tc = Session["session_tc"].ToString();
                Tbl_Musteriler musteri = db.Tbl_Musteriler.Find(tc);
                if (musteri == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(musteri);
                }

            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public ActionResult Profil(FormCollection bilgi)
        {
            if (Session["session_giris"] != null)
            {
                string tc = Session["session_tc"].ToString();
                Tbl_Musteriler musteri = db.Tbl_Musteriler.Find(tc);

                musteri.AdSoyad = bilgi["AdSoyad"].ToString();
                musteri.DogumTarihi = Convert.ToDateTime(bilgi["DogumTarihi"]);
                musteri.Cinsiyet = bilgi["Cinsiyet"].ToString();
                musteri.Telefon = bilgi["Telefon"].ToString();

                db.Entry(musteri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profil");

            }
            return View();
        }

        public ActionResult Cikis()
        {
            Session.Remove("session_tc");
            Session.Clear();
            return RedirectToAction("Giris");
        }

        [HttpGet]
        public ActionResult SifreDegistir()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult SifreDegistir(FormCollection bilgi)
        {
            string tc = Session["session_tc"].ToString() ;
            string sif = GetMD5_2(bilgi["msif"].ToString());
            var count = db.Tbl_Musteriler.Where(x => x.TcKimlik == tc && x.Sifre == sif).Count();

            if (count > 0)
            {
                if (bilgi["ysf1"] == bilgi["ysf2"])
                {
                    string ysif = GetMD5_2(bilgi["ysif1"]).ToString();
                    Tbl_Musteriler musteri = db.Tbl_Musteriler.Find(tc);
                    musteri.Sifre = ysif;
                    db.Entry(musteri).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewData["sonuc"] = "Tebrikler, şifreniz değişti...";
                }
                else
                {
                    ViewData["sonuc"] = "HATA ! Yeni şifreler uyuşmuyor...";
                }

            }
            else
            {
                ViewData["sonuc"] = "HATA ! Mevcut Şifreyi Yanlış Yazdınız ...";
            }
            return View();
        }








    }

}