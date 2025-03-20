using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOS.Department;
using Demo.DAL.Models.Departments;
using Demo.DAL.presistance.Repositories.Departments;
using Demo.DAL.presistance.UnitofWork;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitofwork unitofwork;

        //private readonly IDepartmentRepository departmentRepository;

        //public DepartmentService(IDepartmentRepository departmentRepository)
        //{
        //    this.departmentRepository = departmentRepository;
        //}

        public DepartmentService(IUnitofwork unitofwork)
        {
            this.unitofwork = unitofwork;
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

            var departmetns = unitofwork.DepartmentRepository.GetAllQuery().Where(x => x.IsDeleted == false).Select(d => new DepartmenttoReturnDto {
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
            var department = unitofwork.DepartmentRepository.GetById(id);
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
            var departmentcreated =
                new DAL.Models.Departments.Department()
                {
                    Name = department.Name,
                    Description = department.Description,
                    Code = department.Code,
                    CreationDate = department.CreationDate,
                };
                unitofwork.DepartmentRepository.AddDepartment(departmentcreated);
            return unitofwork.Complete();

        }
        public int DeleteDepartment(int id)
        {
            var departmentRepo = unitofwork.DepartmentRepository;
            var department= departmentRepo.GetById(id);
            if (department == null) return 0;
            departmentRepo.DeleteDepartment(department);
             return unitofwork.Complete();
        }

        

       

        public int UpdateDepartment(DepartmentToUpdateDto department)
        {
            var departmentupdate =
                 new DAL.Models.Departments.Department()
                 {
                     Id = department.Id,
                     Name = department.Name,
                     Description = department.Description,
                     Code = department.Code,
                     CreationDate = department.CreationDate,
                 };
            unitofwork.DepartmentRepository.UpdateDepartment(departmentupdate);
            return unitofwork.Complete();
        }
    }
}
