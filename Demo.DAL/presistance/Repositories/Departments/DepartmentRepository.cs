using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Departments;
using Demo.DAL.presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.presistance.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {


       
        private readonly AppDBContext appDB;

        public DepartmentRepository(AppDBContext appDB) //ask clr to Create object from applicationdbcontext
        {
            this.appDB = appDB;
        }

        public IEnumerable<Department> GetAll(bool AsNoTracing = true)
        {
            if (AsNoTracing) { 
            return appDB.Department.AsNoTracking().ToList();
            }

            return appDB.Department.ToList();

            //unchanged

        }

        public Department? GetById(int id)
        {
           // return appDB.Department.Local.FirstOrDefault(d=>d.Id==id);

            return appDB.Department.Find(id); // search local  in case found ==>return , else send request to database
        }
        public int AddDepartment(Department department)
        {
            appDB.Department.Add(department); // saved locally
          int rowaffected=   appDB.SaveChanges(); // apply remotly

            return rowaffected;
        }

        public int UpdateDepartment(Department department)
        {
            appDB.Department.Update(department); // Modified
            int rowaffected = appDB.SaveChanges(); // unchanged

            return rowaffected;
        }
        public int DeleteDepartment(Department department)
        {
            appDB.Department.Remove(department); // saved locally
            int rowaffected = appDB.SaveChanges(); // apply remotly

            return rowaffected;
        }

        public IQueryable<Department> GetAllQuery()
        {
            return appDB.Department;
            
            
        }
    }
}
