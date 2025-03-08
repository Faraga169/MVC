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
        IEnumerable<DepartmenttoReturnDto> GetAllDepartments();
        DepartmentDetailsToReturnDto? GetDepartmentById(int id);
        int CreateDepartment(DepartmentToCreateDto department);

        int UpdateDepartment(DepartmentToUpdateDto department);

        int DeleteDepartment(int id);

    }
}
