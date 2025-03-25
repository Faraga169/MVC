using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models;
using Demo.DAL.Models.Departments;

namespace Demo.DAL.presistance.Repositories.Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IQueryable<T> GetAllQuery();

        //IEnumerable<T> GetAllEnumerable();
        Task<IEnumerable<T>> GetAllAsync(bool AsNoTracing = true);

        Task<T?> GetByIdAsync(int id);

        void AddDepartment(T department);

        void UpdateDepartment(T  department);

        void DeleteDepartment(T department);

    }
}
