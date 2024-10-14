using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransrodenProyecto.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult IndexAdmin()
        {
            return View();
        }


        public ActionResult IndexTransportista()
        {
            return View();
        }

        public ActionResult AsignTransportista()
        {
            return View();
        }

    }
}
