using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTOS.Department
{
    public class DepartmenttoReturnDto
    {
        public string Name { get; set; } = null!;

        //public string? Description { get; set; }

        public string Code { get; set; } = null!;

        [Display(Name="Creation Date")]
        public DateOnly CreationDate { get; set; }

        public int Id { get; set; }

    }
}
