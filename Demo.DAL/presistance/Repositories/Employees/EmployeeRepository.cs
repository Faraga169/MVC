using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Employees;
using Demo.DAL.presistance.Data;
using Demo.DAL.presistance.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.presistance.Repositories.Employees
{
    public class EmployeeRepository: GenericRepository<DAL.Models.Employees.Employees>, IEmployeeRepository
    {
        public EmployeeRepository(AppDBContext app):base(app)
        {
            
        }




    }
}
