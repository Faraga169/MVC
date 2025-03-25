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
        Task<IEnumerable<EmployeeToReturnDTO>> GetAllEmployeesAsync(string SearchValue);
        Task<EmployeeDetailsToReturnDto?> GetEmployeeByIdAsync(int id);
        Task<int> CreateEmployeeAsync(EmployeeToCreateDto Employee);

        Task<int> UpdateEmployeeAsync(EmployeeToUpdateDto Employee);

        Task<int> DeleteEmployeeAsync(int id);
    }
}