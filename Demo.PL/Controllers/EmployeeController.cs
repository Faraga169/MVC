using Demo.BLL.DTOS.Employee;
using Demo.BLL.Services.Department;
using Demo.BLL.Services.Employee;
using Demo.DAL.Models.Departments;
using Demo.DAL.presistance.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize] // Authenticated is Authorized
    public class EmployeeController:Controller
    {
        private readonly IEmployeeService service;
        private readonly IDepartmentService department;
      
      

        public EmployeeController(IEmployeeService service,IDepartmentService department)
        {
            this.service = service;
            this.department = department;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(string SearchValue)
        {
            
            var employees = await service.GetAllEmployeesAsync(SearchValue);
            return View(employees);
           
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync() {
            // send departments from action of Create to view of Employee [Create]
            ViewData["Departments"] = await department.GetAllDepartmentsAsync();
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateAsync(EmployeeToCreateDto employee) {
            if (!ModelState.IsValid)  //server side validation
            {
                return View(employee);
            }
            var Message = "";
            
                var result = await service.CreateEmployeeAsync(employee);
                if (result > 0)
                {
                TempData["Message"] = "Employee is Added";
                    return RedirectToAction("Index");
                }
                else
                {
                    Message = "Employee is Added";
                TempData["Message"] = Message;
                ModelState.AddModelError("", Message);
                    return View(employee);
                }
            }

          


             
            

        [HttpGet]
        public async Task<IActionResult> DetailsAsync(int? id) {
            if (id is null)
                return BadRequest();

            var result = await service.GetEmployeeByIdAsync(id.Value);
            if (result is null)
                return NotFound();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int?id)
        {
            ViewData["Department"] = await department.GetAllDepartmentsAsync();
            if (id is null)
                return BadRequest();
            
            var result =await service.GetEmployeeByIdAsync(id.Value);
            if (result is null)
                return NotFound();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(int? id,EmployeeDetailsToReturnDto employee) {
            if (id is null)
                return BadRequest();

            var EmployeeUpdate = new EmployeeToUpdateDto() {
                Id=id.Value,
                Name=employee.Name,
                Email=employee.Email,
                Address=employee.Address,
                Phone=employee.Phone,
                Age=employee.Age,
                Salary=employee.Salary,
                Gender=employee.Gender,
                EmployeeType=employee.EmployeeType,
                IsActive=employee.IsActive,
                Department=employee.Department,
                DepartmentId=employee.DepartmentId
            };
            var result = await service.UpdateEmployeeAsync(EmployeeUpdate);
            if (result > 0)
                TempData["Message"] = "Employee is Updated";
            return RedirectToAction("Index");
        
        }


        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id is null)
                return BadRequest();
            var result =await service.GetEmployeeByIdAsync(id.Value);
            if (result is null)
                return NotFound();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        { 
            var result =await service.DeleteEmployeeAsync(id);
            if (result > 0)
                TempData["Message"] = "Employee is Deleted";
            return RedirectToAction("Index");
            return View(nameof(Index));
        }
    }

    
}
