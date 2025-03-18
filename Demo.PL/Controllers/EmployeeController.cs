using Demo.BLL.DTOS.Employee;
using Demo.BLL.Services.Department;
using Demo.BLL.Services.Employee;
using Demo.DAL.Models.Departments;
using Demo.DAL.presistance.Data;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
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
        public IActionResult Index(string SearchValue)
        {
            
            var employees = service.GetAllEmployees(SearchValue);
            return View(employees);
           
        }

        [HttpGet]
        public IActionResult Create() {
            // send departments from action of Create to view of Employee [Create]
            ViewData["Departments"] = department.GetAllDepartments();
            return View();
        }

        [HttpPost]

        public IActionResult Create(EmployeeToCreateDto employee) {
            if (!ModelState.IsValid)  //server side validation
            {
                return View(employee);
            }
            var Message = "";
            
                var result = service.CreateEmployee(employee);
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
        public IActionResult Details(int? id) {
            if (id is null)
                return BadRequest();

            var result = service.GetEmployeeById(id.Value);
            if (result is null)
                return NotFound();

            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(int?id)
        {
            ViewData["Department"] = department.GetAllDepartments();
            if (id is null)
                return BadRequest();
            
            var result = service.GetEmployeeById(id.Value);
            if (result is null)
                return NotFound();
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(int? id,EmployeeDetailsToReturnDto employee) {
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
            var result = service.UpdateEmployee(EmployeeUpdate);
            if (result > 0)
                TempData["Message"] = "Employee is Updated";
            return RedirectToAction("Index");
        
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var result = service.GetEmployeeById(id.Value);
            if (result is null)
                return NotFound();
            return View(result);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        { 
            var result = service.DeleteEmployee(id);
            if (result > 0)
                TempData["Message"] = "Employee is Deleted";
            return RedirectToAction("Index");
            return View(nameof(Index));
        }
    }

    
}
