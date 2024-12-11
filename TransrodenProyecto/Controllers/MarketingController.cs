using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TransrodenProyecto.Models;

namespace TransrodenProyecto.Controllers
{
    public class MarketingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Marketing
        public ActionResult Marketing()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EnviarBoletin()
        {
            return View();
        }
        public ActionResult CorreoEnviado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnviarBoletin(string Asunto, string Mensaje, HttpPostedFileBase Imagen)
        {
            try
            {
                // Obtener los usuarios con el rol de Cliente y las notificaciones activas

                var usuarios = db.Usuarios.Where(u => u.Rol == Rol.Cliente && u.NotifCli).ToList();


                if (usuarios.Count == 0)
                {
                    return Content("No se encontraron usuarios con el rol de Cliente.");
                }

                // Configurar el cliente SMTP
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("anthonyibarrasperez@gmail.com", "swtg adap vlig xwqi"),
                    EnableSsl = true,
                };

                // Crear el mensaje de correo
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("anthonyibarrasperez@gmail.com"),
                    Subject = Asunto,
                    Body = Mensaje,
                    IsBodyHtml = true,
                };

                // Agregar los destinatarios (usuarios con rol Cliente)
                foreach (var usuario in usuarios)
                {
                    mailMessage.To.Add(usuario.Correo);
                }

                // Adjuntar imagen si se ha proporcionado
                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(Imagen.FileName);
                    mailMessage.Attachments.Add(new Attachment(Imagen.InputStream, fileName));
                }

                // Enviar el correo
                smtpClient.Send(mailMessage);
                return RedirectToAction("CorreoEnviado");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return Content($"Error al enviar el boletín: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult EnviarImportante(string Asunto, string Mensaje, HttpPostedFileBase Imagen)
        {
            try
            {
                // Obtener todos los usuarios registrados
                var usuarios = db.Usuarios.ToList();

                if (usuarios.Count == 0)
                {
                    return Content("No se encontraron usuarios registrados.");
                }

                // Configurar el cliente SMTP
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("anthonyibarrasperez@gmail.com", "swtg adap vlig xwqi"),
                    EnableSsl = true,
                };

                // Crear el mensaje de correo
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("anthonyibarrasperez@gmail.com"),
                    Subject = Asunto,
                    Body = Mensaje,
                    IsBodyHtml = true,
                };

                // Agregar los destinatarios (todos los usuarios)
                foreach (var usuario in usuarios)
                {
                    mailMessage.To.Add(usuario.Correo);
                }

                // Adjuntar imagen si se ha proporcionado
                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(Imagen.FileName);
                    mailMessage.Attachments.Add(new Attachment(Imagen.InputStream, fileName));
                }

                // Enviar el correo
                smtpClient.Send(mailMessage);
                return RedirectToAction("CorreoEnviado");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return Content($"Error al enviar el correo: {ex.Message}");
            }

        }


    }
}