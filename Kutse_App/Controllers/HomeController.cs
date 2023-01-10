using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Ootan sind minu peole! Palun tule!";
            int month = DateTime.Now.Month;
           /* ViewBag.Greeting = month == 1 ? "Rahupäev" : ViewBag.Greeting = month == 2 ? "Pehme padja päev" : ViewBag.Greeting = month == 3 ? "Šokolaadi kirsipäev" : 
                ViewBag.Greeting = month == 4 ? "spagettide päev" : ViewBag.Greeting = month == 5 ? "";*/
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour >= 0 && hour <= 6 ? "Head õõd" : ViewBag.Greeting = hour >= 6 && hour <= 12 ? "Tere hommikust" :
                ViewBag.Greeting = hour >= 12 && hour <= 18 ? "Head päevast" : "Tere õhtust";
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
            { return View("Thanks", guest); }
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
    }
}