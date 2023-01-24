using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {              
            int month = DateTime.Now.Month;
            ViewBag.Greeting2 = month == 1 ? "rahupäev" : ViewBag.Greeting2 = month == 2 ? "pehme padja päev" : ViewBag.Greeting2 = month == 3 ? "šokolaadi kirsipäev" : 
                ViewBag.Greeting2 = month == 4 ? "spagettide päev" : ViewBag.Greeting2 = month == 5 ? "päev 5" : ViewBag.Greeting2 = month == 6 ? "päev 6" : ViewBag.Greeting2 = month == 7 ? "päev 7"
                : ViewBag.Greeting2 = month == 8 ? "päev 8" : ViewBag.Greeting2 = month == 9 ? "päev 9" : ViewBag.Greeting2 = month == 10 ? "päev 10" : ViewBag.Greeting2 = month == 11 ? "päev 11"
                : "päev 12";
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour >= 0 && hour <= 6 ? "Head õõd" : ViewBag.Greeting = hour >= 6 && hour <= 12 ? "Tere hommikust" :
                ViewBag.Greeting = hour >= 12 && hour <= 18 ? "Tere päevast" : "Tere õhtust";
            return View();
        }
        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            if (ModelState.IsValid)
            { 
                db.Guests.Add(guest);
                db.SaveChanges();
                return View("Thanks", guest); 
            }
            else
            { return View(); }
        }
        public void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "marklarionov469@gmail.com";
                WebMail.Password = "oojzvaoqyalwqude";
                WebMail.From = "marklarionov469@gmail.com";
                WebMail.Send("marklarionov469@gmail.com", "Vastus kutsele", guest.Name + " vastas " + ((guest.WillAttend ?? false) ?
                    "tuleb peole " : "ei tule peole"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahju!Ei saa kirja saada!";
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        public ActionResult Delete(int id) 
        {
            Guest g = db.Guests.Find(id);
            if (g==null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) 
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }


        GuestContext db = new GuestContext();
        [Authorize]
        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }
    }
}