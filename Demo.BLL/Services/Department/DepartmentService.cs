using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOS.Department;
using Demo.DAL.Models.Departments;
using Demo.DAL.presistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        public IEnumerable<DepartmenttoReturnDto> GetAllDepartments()
        {
            //var departments=departmentRepository.GetAll();   // IEnumerableDepartment=====> IEnumerableDepartmentDTo
            //                                                // Department ===> DepartmenttoReturnDto (Map)
            //foreach (var department in departments) {
            //    yield return new DepartmenttoReturnDto()
            //    {
            //        Id = department.Id,
            //        Name = department.Name,
            //        Description = department.Description,
            //        Code = department.Code,
            //        CreationDate = department.CreationDate
            //    };
            //}

            var departmetns = departmentRepository.GetAllQuery().Select(d => new DepartmenttoReturnDto {
                Id = d.Id,
                Name = d.Name,
                //Description = d.Description,
                Code = d.Code,
                CreationDate = d.CreationDate
            }).AsNoTracking().ToList();

            return departmetns;
            
        }

        public DepartmentDetailsToReturnDto? GetDepartmentById(int id)
        {
            var department = departmentRepository.GetById(id);
            if (department == null) return null;
            return new DepartmentDetailsToReturnDto() { 
            Description = department.Description,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn,
                Id = department.Id,
                IsDeleted = department.IsDeleted,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn
            };
        }
        public int CreateDepartment(DepartmentToCreateDto department)
        {
            var departmentcreated = departmentRepository.AddDepartment(
                new DAL.Models.Departments.Department
                {
                    Name = department.Name,
                    Description = department.Description,
                    Code = department.Code,
                    CreationDate = department.CreationDate, 
                }
                );
            return departmentcreated;

        }
        public int DeleteDepartment(int id)
        {
            var department= departmentRepository.GetById(id);
            if (department == null) return 0;
            return departmentRepository.DeleteDepartment(department);
        }

        

       

        public int UpdateDepartment(DepartmentToUpdateDto department)
        {
            var departmentupdate = departmentRepository.AddDepartment(
                 new DAL.Models.Departments.Department
                 {
                     Id = department.Id,
                     Name = department.Name,
                     Description = department.Description,
                     Code = department.Code,
                     CreationDate = department.CreationDate,
                 }
                 );
            return departmentupdate;
        }
    }
}
