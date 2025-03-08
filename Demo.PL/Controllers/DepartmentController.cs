using Demo.BLL.DTOS.Department;
using Demo.BLL.Services.Department;
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
        public IActionResult Details()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Details(int id)
        //{
        //    var department = service.GetDepartmentById(id);
        //    if (department == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(department);
        //}
    }
}
