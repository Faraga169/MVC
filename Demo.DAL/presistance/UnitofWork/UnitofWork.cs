using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.presistance.Data;
using Demo.DAL.presistance.Repositories.Departments;
using Demo.DAL.presistance.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.presistance.UnitofWork
{
    public class UnitofWork : IUnitofwork
    {
        private readonly AppDBContext unitofwork;

        public UnitofWork(AppDBContext unitofwork)
        {
            this.unitofwork = unitofwork;
        //    EmployeeRepository = new EmployeeRepository(unitofwork);
        //    DepartmentRepository = new DepartmentRepository(unitofwork);
        }
        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(unitofwork);
        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(unitofwork);

       
   

        public int Complete()
        {
            return unitofwork.SaveChanges();
        }

        public void Dispose() { 
        unitofwork.Dispose();
        }
    }
}
