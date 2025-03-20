using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models;
using Demo.DAL.Models.Departments;
using Demo.DAL.presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.presistance.Repositories.Generic
{
    


    public class GenericRepository<T>:IGenericRepository<T> where T :ModelBase
    {
        private readonly AppDBContext appDB;

      
        public GenericRepository(AppDBContext appDB) //ask clr to Create object from applicationdbcontext
        {
            this.appDB = appDB;
        }

        public IEnumerable<T> GetAll(bool AsNoTracing = true)
        {
            // IsDeleted== false ==> not deleted (Appear to user)
            if (AsNoTracing)
            {
                return appDB.Set<T>().Where(x=>x.IsDeleted==false).AsNoTracking().ToList();
            }

            
            return appDB.Set<T>().Where(x=>x.IsDeleted==false).ToList();

            //unchanged

        }

        public T? GetById(int id)
        {
            // return appDB.Department.Local.FirstOrDefault(d=>d.Id==id);

            return appDB.Set<T>().Find(id); // search local  in case found ==>return , else send request to database
        }
        public void AddDepartment(T department)
        {
            appDB.Set<T>().Add(department); // saved locally
            //int rowaffected = appDB.SaveChanges(); // apply remotly

            //return rowaffected;
        }

        public void UpdateDepartment(T department)
        {
            appDB.Set<T>().Update(department); // Modified
            //int rowaffected = appDB.SaveChanges(); // unchanged

            //return rowaffected;
        }
        public void DeleteDepartment(T department)
        {
            //appDB.Set<T>().Remove(department); // saved locally
            //int rowaffected = appDB.SaveChanges(); // apply remotly

            //return rowaffected;

            //IsDeleted == true ==> deleted (Disapear from user)
            department.IsDeleted = true;
            appDB.Set<T>().Update(department);
            //return appDB.SaveChanges();
        }

        public IQueryable<T> GetAllQuery()
        {
            return appDB.Set<T>();


        }

        

        //public IEnumerable<T> GetAllEnumerable()
        //{
        //    return appDB.Set<T>();
        //}
    }
}
