using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.presistance.Repositories.Departments;

namespace Demo.DAL.presistance.UnitofWork
{
    public interface IUnitofwork:IAsyncDisposable
    {
        public  IEmployeeRepository EmployeeRepository { get;  }

        public IDepartmentRepository DepartmentRepository { get; }

        Task<int> CompleteAsync();
    }
}
