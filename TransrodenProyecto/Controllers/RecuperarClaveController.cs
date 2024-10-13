using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using TransrodenProyecto.Models;

namespace TransrodenProyecto.Controllers
{
    public class RecuperarClaveController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult RecuperarClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarClave(string correo)
        {
            // Buscar el usuario en la base de datos
            var usuario = db.Usuarios.FirstOrDefault(u => u.Correo == correo);

            if (usuario == null)
            {
                // Si no se encuentra el correo
                return Content("El correo no está registrado.");
            }

            try
            {
                // Configurar el cliente SMTP
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("anthonyibarrasperez@gmail.com", "swtg adap vlig xwqi"), //Se cambiaria al correo de la empresa c:
                    EnableSsl = true,
                };

                // Crear el mensaje de correo
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("anthonyibarrasperez@gmail.com"),
                    Subject = "Recuperación de contraseña",
                    Body = $"Tu contraseña es: {usuario.Clave}",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(usuario.Correo);

                // Enviar el correo
                smtpClient.Send(mailMessage);

                return Content("Se ha enviado la contraseña a tu correo.");
            }
            catch (Exception ex)
            {
                // Manejar cualquier error durante el envío del correo
                return Content($"Error al enviar el correo: {ex.Message}");
            }

            

        }

    }
}