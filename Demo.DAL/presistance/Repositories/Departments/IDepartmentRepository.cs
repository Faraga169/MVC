using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Departments;

namespace Demo.DAL.presistance.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> GetAllQuery();
        IEnumerable<Department> GetAll(bool AsNoTracing=true);

        Department? GetById(int id);

      int AddDepartment (Department department);

      int UpdateDepartment(Department department);

      int DeleteDepartment(Department department);
       



    }
}
