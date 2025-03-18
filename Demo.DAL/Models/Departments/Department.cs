using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Employees;

namespace Demo.DAL.Models.Departments
{
    public class Department:ModelBase  // Department is a ModelBase
    {
        

        public string? Name { get; set; }=null!;

        public string? Description { get; set; }

        public string Code { get; set; }= null!;

        public DateOnly CreationDate { get; set; }

        // Navigation property
        public virtual ICollection<Demo.DAL.Models.Employees.Employees> Employees { get; set; } = new HashSet<Demo.DAL.Models.Employees.Employees>();
    }
}
