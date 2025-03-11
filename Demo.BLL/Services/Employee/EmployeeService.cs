using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOS.Employee;
using Demo.DAL.Models.Common.Enum;
using Demo.DAL.Models.Employees;
using Demo.DAL.presistance.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository service;

        public EmployeeService(IEmployeeRepository service)
        {
            this.service = service;
        }

        public IEnumerable<EmployeeToReturnDTO> GetAllEmployees()
        {

         return service.GetAllQuery().Where(x=>x.IsDeleted==false).Select(E =>new EmployeeToReturnDTO()
            {
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
         });

           
        }

        public EmployeeDetailsToReturnDto? GetEmployeeById(int id)
        {
            var employee= service.GetById(id);
            if (employee == null) return null;
            return new EmployeeDetailsToReturnDto()
            {
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
            };


        }

        public int CreateEmployee(EmployeeToCreateDto Employee)
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
            };
           return service.AddDepartment(employee);
        }

        public int DeleteEmployee(int id)
        {
            var employee = service.GetById(id);
            if (employee == null) return 0;
            return service.DeleteDepartment(employee);
        }

       

        public int UpdateEmployee(EmployeeToUpdateDto Employee)
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
                HiringDate = Employee.HiringDate,
                Gender = (Gender)Enum.Parse(typeof(Gender),Employee.Gender),
                EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), Employee.EmployeeType),
            };
            return service.UpdateDepartment(Employees);
           
               
   

            
        }
    }
}
