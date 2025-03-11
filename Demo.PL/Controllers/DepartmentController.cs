using Demo.BLL.DTOS.Department;
using Demo.BLL.Services.Department;
using Demo.DAL.Models.Departments;
using Demo.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{

    //DepartmentController : Inheritance [is a controller]
    //DepartmentController : Composition  [has a Department service]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService service;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment enviroment;

        public DepartmentController(IDepartmentService service,ILogger<DepartmentController>logger,IWebHostEnvironment enviroment)
        {
            this.service = service;
            this.logger = logger;
            this.enviroment = enviroment;
        }


        //Action ==> Master Action

        [HttpGet]
        public IActionResult Index()
        {
            var departments = service.GetAllDepartments();
            return View(departments);
        }

        [HttpGet] //Show The Form
        public IActionResult Create() {

            return View();

        }

        [HttpPost] //Save Data
        public IActionResult Create(DepartmentToCreateDto department) {
            if (!ModelState.IsValid)  //server side validation
            {
                return View(department);
            }
            var Message = "";
            try
            {
                    var result=service.CreateDepartment(department);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else {
                    Message = "Department can not be created";
                    ModelState.AddModelError("", Message);
                    return View(department);
                }
            }

            catch (Exception ex) { 

                //log exception

                logger.LogError(ex, ex.Message);
                if(enviroment.IsDevelopment())
                {
                   Message=ex.Message;
                    return View(department);
                }
                else
                {
                    Message="Department can not be Created";
                    return View("Error");
                }

            }
        
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null) {
                return BadRequest(); //400
            }

            var department = service.GetDepartmentById(id.Value);

            if (department == null) {
                return NotFound(); //404
    ;            }
            return View(department);
        }


        [HttpGet]
        public IActionResult Edit(int? id) {
            if (id is null) { 
            return BadRequest();
            }
            var department = service.GetDepartmentById(id.Value);

            if (department == null)
            {
                return NotFound();
            }
            return View(new DepartmentEditViewModel() { 
            Code = department.Code,
                CreationDate = department.CreationDate,
                Description = department.Description,
                Name = department.Name
            });
        }

        [HttpPost]
        public IActionResult Edit(int id,DepartmentEditViewModel departmentS)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentS);
            }
            var Message = "";
            try {
                var UpdateDepartment = new DepartmentToUpdateDto()
                {
                    Id = id,
                    Name = departmentS.Name,
                    Code = departmentS.Code,
                    CreationDate = departmentS.CreationDate,
                    Description = departmentS.Description
                };
                var department = service.UpdateDepartment(UpdateDepartment);
                if (department > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Message = "Department can not be updated";
                    ModelState.AddModelError("", Message);
                    return View(departmentS);
                }
               
            }
            catch (Exception ex) {
            Message=enviroment.IsDevelopment() ? ex.Message : "Department can not be updated";
                logger.LogError(ex, ex.Message);
                return View(departmentS);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id) {
            if (id is null)
            {
                return BadRequest();
            }
            var department = service.GetDepartmentById(id.Value);
            if (department == null)
                return NotFound();
            
            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(int id) {

            var result = service.DeleteDepartment(id);
            var Message = "";
            try {
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                  Message = "Department can not be deleted";
                    ModelState.AddModelError("", Message);
                    return View();
                }
            }

            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Message= enviroment.IsDevelopment() ? ex.Message : "Department can not be deleted";
            }

            return View(nameof(Index));
        }  
    }
}
