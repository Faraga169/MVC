﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Demo.DAL.Models.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FName { get; set; }

        public string LName { get; set; }

        public bool IsAgree { get; set; }

    }
}
