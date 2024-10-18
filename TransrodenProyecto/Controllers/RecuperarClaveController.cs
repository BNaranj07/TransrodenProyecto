using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
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


        public ActionResult CambioClave()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RecuperarClave(string correo)
        {
            // Cambio: Buscar el usuario en la base de datos
            var usuario = db.Usuarios.FirstOrDefault(u => u.Correo == correo);
            if (usuario == null)
            {
                return Content("El correo no está registrado.");
            }

            try
            {
                // Cambio: Generar una nueva clave
                string nuevaClavesinEncriptar = GenerarClaveSegura();

                // Cambio: Encriptar la nueva clave
                string nuevaClaveEncriptada = CuentaController.ConvertirSha256(nuevaClavesinEncriptar);

                // Cambio: Actualizar la clave del usuario en la base de datos
                usuario.Clave = nuevaClaveEncriptada;
                db.SaveChanges();

                // Configurar el cliente SMTP (sin cambios)
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("anthonyibarrasperez@gmail.com", "swtg adap vlig xwqi"),
                    EnableSsl = true,
                };

                // Cambio: Modificar el cuerpo del mensaje para enviar la clave sin encriptar
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("anthonyibarrasperez@gmail.com"),
                    Subject = "Recuperación de contraseña",
                    Body = $"Tu nueva contraseña es: {nuevaClavesinEncriptar}",
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(usuario.Correo);

                // Enviar el correo (sin cambios)
                smtpClient.Send(mailMessage);

                //return Content("Se ha enviado una nueva contraseña a tu correo.");
                return RedirectToAction("CambioClave");
            }
            catch (Exception ex)
            {
                // Manejar cualquier error durante el proceso
                return Content($"Error al procesar la solicitud: {ex.Message}");
            }
        }



        private string GenerarClaveSegura(int longitud = 12)
        {
            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=<>?";
            StringBuilder resultado = new StringBuilder();

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (longitud-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    resultado.Append(caracteresPermitidos[(int)(num % (uint)caracteresPermitidos.Length)]);
                }
            }

            return resultado.ToString();
        }

    }
}