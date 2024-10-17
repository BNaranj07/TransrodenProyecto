using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TransrodenProyecto.Models;
using TransrodenProyecto.Controllers;
using System.Threading;

namespace TransrodenProyecto.Controllers
{
    public class CuentaController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                var existingUser = db.Usuarios.FirstOrDefault(u => u.Correo == usuario.Correo);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "El correo ya está registrado.");
                    return View(usuario);
                }

                usuario.Clave = ConvertirSha256(usuario.Clave);


                usuario.Rol = Rol.Cliente;


                db.Usuarios.Add(usuario);
                db.SaveChanges();
                Thread.Sleep(3000);

                return RedirectToAction("Login", "Cuenta");
            }

            return View(usuario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string correo, string clave)
        {
            var usuario = db.Usuarios.FirstOrDefault(u => u.Correo == correo);

            if (usuario != null)
            {
                // Verificar si el usuario está bloqueado
                if (usuario.LockoutEnabled && usuario.LockoutEnd.HasValue && usuario.LockoutEnd.Value > DateTime.Now)
                {
                    ModelState.AddModelError("", "Su cuenta está bloqueada. Intente de nuevo más tarde.");
                    return View();
                }

                // Encriptar la clave ingresada
                string ClaveEncryt = ConvertirSha256(clave);

                // Verificar si la clave es correcta
                if (usuario.Clave == ClaveEncryt)
                {
                    // Restablecer el contador de intentos fallidos
                    usuario.AccessFailedCount = 0;
                    usuario.LockoutEnd = null;
                    db.SaveChanges();

                    // Iniciar sesión
                    Session["UsuarioId"] = usuario.Id_Usuario;
                    Session["UsuarioRol"] = usuario.Rol;
                    Session["Usuario"] = $"{usuario.Nombre}";

                    // Redireccionar según el rol del usuario
                    if (usuario.Rol == Rol.Administrador || usuario.Rol == Rol.Bodeguero)
                    {
                        return RedirectToAction("IndexAdmin", "Admin");
                    }
                    else if (usuario.Rol == Rol.Cliente)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (usuario.Rol == Rol.Transportista)
                    {
                        return RedirectToAction("IndexTransportista", "Admin");
                    }
                }
                else
                {
                    // Incrementar el contador de intentos fallidos
                    usuario.AccessFailedCount++;

                    // Si el usuario ha fallado 2 veces, bloquear la cuenta por 1 minuto
                    if (usuario.AccessFailedCount >= 2)
                    {
                        usuario.LockoutEnd = DateTime.Now.AddMinutes(1);
                    }
                    db.SaveChanges();

                    ModelState.AddModelError("", "Correo o clave incorrectos.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Correo o clave incorrectos.");
            }

            return View();
        }



        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Cuenta");
        }



        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        
    }
}
