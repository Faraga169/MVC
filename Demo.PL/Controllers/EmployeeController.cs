using Demo.BLL.DTOS.Employee;
using Demo.BLL.Services.Employee;
using Demo.DAL.Models.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController:Controller
    {
        private readonly IEmployeeService service;

        public EmployeeController(IEmployeeService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = service.GetAllEmployees();
            return View(employees);
           
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]

        public IActionResult Create(EmployeeToCreateDto employee) {
            if (!ModelState.IsValid)  //server side validation
            {
                return View(employee);
            }
            var Message = "";
            try
            {
                var result = service.CreateEmployee(employee);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Message = "Department can not be created";
                    ModelState.AddModelError("", Message);
                    return View(employee);
                }
            }

            catch (Exception ex)
            {

            }

            return View(employee);

                //log exception
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
        public IActionResult Edit(int?id) { 
            if(id is null)
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
                Age=employee.Age,
                Salary=employee.Salary,
                Gender=employee.Gender,
                EmployeeType=employee.EmployeeType,
                IsActive=employee.IsActive
            };
            var result = service.UpdateEmployee(EmployeeUpdate);
            if (result > 0)
                return RedirectToAction("Index");
            return View(employee);
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
                return RedirectToAction("Index");
            return View(nameof(Index));
        }
    }

    
}
