using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Identity;

namespace Demo.BLL.Common.Services.EmailServices
{
    public class EmailSetting : IEmailSettings
    {
        public void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            // Sender , reciever
            // Mariam@gmail.com, user Who to reset password
            client.Credentials = new NetworkCredential("faragfekryahmed@gmail.com", "nfdzzbersjwqkyjq");// generate password
            client.Send("faragfekryahmed@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
