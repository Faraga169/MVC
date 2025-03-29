using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Identity;

namespace Demo.BLL.Common.Services.EmailServices
{
    public interface IEmailSettings
    {
        public void SendEmail(Email email);
    }
}
