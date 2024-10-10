using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransrodenProyecto.Models;

namespace TransrodenProyecto.Permisos
{
    public class PermisosRolAttribute : ActionFilterAttribute
    {
        private Rol idrol;


        public PermisosRolAttribute(Rol _idrol)
        {

            idrol = _idrol;
        }


        /*
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Usuario"] != null)
            {
                Usuario usuario = HttpContext.Current.Session["Usuario"] as Usuario;


                if (usuario.Rol != this.idrol)
                {

                    filterContext.Result = new RedirectResult("~/Home/SinPermiso");

                }


            }



            base.OnActionExecuting(filterContext);
        }*/
        


    }
}