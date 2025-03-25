using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOS.Department;
using Demo.DAL.presistance.Repositories.Departments;

namespace Demo.BLL.Services.Department
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmenttoReturnDto>> GetAllDepartmentsAsync();
        Task<DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int id);
        Task<int> CreateDepartmentAsync(DepartmentToCreateDto department);

        Task<int> UpdateDepartmentAsync(DepartmentToUpdateDto department);

        Task<int> DeleteDepartmentAsync(int id);

    }
}
