﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Departments;
using Demo.DAL.presistance.Data;
using Demo.DAL.presistance.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.presistance.Repositories.Departments
{
    public class DepartmentRepository :GenericRepository<Department>,IDepartmentRepository
    {

        public DepartmentRepository(AppDBContext app):base(app)
        {
            
        }


    }
}
