using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using System.IO;



namespace CapaNegocio
{
    public class CN_Helper
    {
        // Generates a random password that its sent to the user's email when the person is registered for the first time
        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }

        // Encrypts the password using SHA256 to save it in the database
        public static string ConvertirSHA256(string text)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(text));

                foreach(byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        // Sends an email to the user with the password generated when the person is registered for the first time
        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                // Configures the new mail
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("myxoonlineshop@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                // Configres the Gmail SMTP server
                var smtp = new SmtpClient() 
                {
                    Credentials = new NetworkCredential("myxoonlineshop@gmail.com", "vocuiooyltmghqtl"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                }; 

                smtp.Send(mail);

                resultado = true;

            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }

    }
}
