using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Common.Enum;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.DTOS.Employee
{
    public class EmployeeToUpdateDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max Length should be 50 characters")]
        [MinLength(10, ErrorMessage = "Min Length should be 10 characters")]
        public string Name { get; set; } = null!;

        [Range(22, 30)]
        public int? Age { get; set; }

        //public DateOnly HiringDate { get; set; }
        public string? Address { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }

        
        [Phone]
        public int Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string Gender { get; set; }

        public string EmployeeType { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

        public string? Department{ get; set; }

        public IFormFile?  Image { get; set; }
    }
}



