using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOS.Employee;

namespace Demo.BLL.Services.Employee
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeToReturnDTO> GetAllEmployees();
        EmployeeDetailsToReturnDto? GetEmployeeById(int id);
        int CreateEmployee(EmployeeToCreateDto Employee);

        int UpdateEmployee(EmployeeToUpdateDto Employee);

        int DeleteEmployee(int id);
    }
}