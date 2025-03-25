using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using Demo.BLL.Common.Services.AttachmentService;
using Demo.BLL.DTOS.Employee;
using Demo.DAL.Models.Common.Enum;
using Demo.DAL.Models.Employees;
using Demo.DAL.presistance.Repositories.Employees;
using Demo.DAL.presistance.UnitofWork;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitofwork unitofwork;
        private readonly IAttachmentService attachment;

        //private readonly IEmployeeRepository service;

        //public EmployeeService(IEmployeeRepository service)
        //{
        //    this.service = service;
        //}

        public EmployeeService(IUnitofwork unitofwork,IAttachmentService attachment)
        {
            this.unitofwork = unitofwork;
            this.attachment = attachment;
        }


        public async Task<IEnumerable<EmployeeToReturnDTO>> GetAllEmployeesAsync(string Searchvalue)
        {

        var query= await unitofwork.EmployeeRepository.GetAllQuery().Include(E=>E.Department).Where(x=>x.IsDeleted==false && (String.IsNullOrEmpty(Searchvalue) ||x.Name.ToLower().Contains(Searchvalue.ToLower()) )).Select(E =>new EmployeeToReturnDTO()
            {
              Image=E.ImageUrl,
              Id = E.Id,
              Name = E.Name,
              Age= E.Age,
              Phone=E.PhoneNumber,
              Email= E.Email,
              Address=E.Address,
              Salary= E.Salary,
              Gender=E.Gender.ToString(),
              EmployeeType=E.EmployeeType.ToString(),
              IsActive=E.IsActive,
              department=E.Department.Name  // use eager loading
         }).ToListAsync();
            return  query;

            #region Such For IQuerable vs IEnumerable
            //var employees = query.ToList();
            // var count = query.Count();
            // var firstEmployee = query.FirstOrDefault();
            // return query;
            #endregion


        }

        public async Task<EmployeeDetailsToReturnDto?> GetEmployeeByIdAsync(int id)
        {
            var employee= await unitofwork.EmployeeRepository.GetByIdAsync(id);
            if (employee == null) return null;
            return new EmployeeDetailsToReturnDto()
            {
                Image= employee.ImageUrl,
                Name = employee.Name,
                Age = employee.Age,
                Phone = employee.PhoneNumber,
                Email = employee.Email,
                Salary = employee.Salary,
                Address = employee.Address,
                Id = id,
                Gender = employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
                IsActive = employee.IsActive,
                Department = employee?.Department?.Name // use lazy loading
            };


        }

        public async Task<int> CreateEmployeeAsync(EmployeeToCreateDto Employee)
        {


            Employees employee=new Employees()
            {
                
                Name = Employee.Name,
                Address = Employee.Address,
                Age = Employee.Age,
                Email = Employee.Email,
                IsActive = Employee.IsActive,
                Salary = Employee.Salary,
                PhoneNumber = Employee.Phone,
                HiringDate = Employee.HiringDate,
                Gender=Employee.Gender,
                EmployeeType = Employee.EmployeeType,
                DepartmentId= Employee.DepartmentId,
            };
            if(Employee.Image is not null)
                employee.ImageUrl = await attachment.UploadAsync(Employee.Image,"images");
            unitofwork.EmployeeRepository.AddDepartment(employee);
             return await unitofwork.CompleteAsync();  // rows affected
        }

        public async  Task<int> DeleteEmployeeAsync(int id)
        {
            var employee = await unitofwork.EmployeeRepository.GetByIdAsync(id);
            return await unitofwork.CompleteAsync();  //rows affected
        }

       

        public async Task<int> UpdateEmployeeAsync(EmployeeToUpdateDto Employee)
        {
            var Employees = new Employees()
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Address = Employee.Address,
                Age = Employee.Age,
                Email = Employee.Email,
                IsActive = Employee.IsActive,
                Salary = Employee.Salary,
               PhoneNumber= Employee.Phone,
                Gender = Employee.Gender != null ? (Gender)Enum.Parse(typeof(Gender), Employee.Gender) : default(Gender),
                EmployeeType = Employee.EmployeeType != null ? (EmployeeType)Enum.Parse(typeof(EmployeeType), Employee.EmployeeType) : default(EmployeeType),
                DepartmentId=Employee.DepartmentId,
                
        
            };
            unitofwork.EmployeeRepository.UpdateDepartment(Employees);
           return await unitofwork.CompleteAsync();


   


    }
    }
}
