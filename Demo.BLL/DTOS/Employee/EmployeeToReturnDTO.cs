using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Departments;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.DTOS.Employee
{
    public class EmployeeToReturnDTO
    {
        // Get All ====> Employee Service
        public int Id { get; set; }
        public string? department { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; }
        public int? Phone { get; set; }
        public string EmployeeType { get; set; }
        public string? Image { get; set; }
    }
}
