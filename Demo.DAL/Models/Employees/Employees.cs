using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Common.Enum;

namespace Demo.DAL.Models.Employees
{
    public class Employees:ModelBase
    {
        public string Name { get; set; } = null!;

        public int? Age{ get; set; }
        public string? Address { get; set; }

        public string? Email { get; set; }
        public decimal Salary { get; set; }

        public  bool IsActive { get; set; }

        public int? PhoneNumber { get; set; }

        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }




    }
}
