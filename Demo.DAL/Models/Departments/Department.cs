using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models.Departments
{
    public class Department:ModelBase  // Department is a ModelBase
    {
        public string Name { get; set; }=null!;

        public string? Description { get; set; }

        public string Code { get; set; }= null!;

        public DateOnly CreationDate { get; set; }
    }
}
