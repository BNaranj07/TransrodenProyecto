using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TransrodenProyecto.Models;

namespace TransrodenProyecto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        public ActionResult Nosotros()
        {
            return View();
        }


        public ActionResult Sucursales()
        {
            return View();
        }


        public ActionResult Tracking()
        {
            return View();
        }





        public ActionResult IndexAdmin()
        {
            return View();
        }


        public ActionResult IndexTransportista()
        {
            return View();
        }


    }
}