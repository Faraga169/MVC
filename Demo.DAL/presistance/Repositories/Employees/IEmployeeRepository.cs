using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Employees;
using Demo.DAL.presistance.Repositories.Generic;


public interface IEmployeeRepository:IGenericRepository<Employees>
{
       
    }

