using AutoMapper;
using Demo.BLL.DTOS.Department;
using Demo.BLL.Services.Department;
using Demo.DAL.Models.Departments;
using Demo.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace Demo.PL.Controllers
{

    //DepartmentController : Inheritance [is a controller]
    //DepartmentController : Composition  [has a Department service]
    public class DepartmentController : Controller
    {
        private readonly IMapper mapper;
        private readonly IDepartmentService service;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment enviroment;

        public DepartmentController(IMapper mapper,IDepartmentService service,ILogger<DepartmentController>logger,IWebHostEnvironment enviroment)
        {
            this.mapper = mapper;
            this.service = service;
            this.logger = logger;
            this.enviroment = enviroment;
        }


        //Action ==> Master Action

        [HttpGet]
        public IActionResult Index()
        {
            //ViewData["Message"] = "Hello From view data";
            ViewBag.Message = "Hello From view bag";
            var departments = service.GetAllDepartments();
            return View(departments);
        }

        [HttpGet] //Show The Form
        public IActionResult Create() {


            return View();

        }

        [HttpPost] //Save Data
        [ValidateAntiForgeryToken]// Action Filter
        public IActionResult Create(DepartmentViewModel department) {
            if (!ModelState.IsValid)  //server side validation
            {
                return View(department);
            }
            var Message = "";
            try
            {
                #region Auto Mapper Automatic
                var departmentToCreated = mapper.Map<DepartmentViewModel, DepartmentToCreateDto>(department);
                var result = service.CreateDepartment(departmentToCreated);
                #endregion

                #region Mapping Manually

                //var result=service.CreateDepartment(new DepartmentToCreateDto() { 
                //Name = department.Name,
                //Code = department.Code,
                //CreationDate = department.CreationDate,
                //Description = department.Description

                //}); 
                #endregion

                if (result > 0)
                {
                    TempData["Message"] = "Department is Created";
                    return RedirectToAction("Index");
                }
                else {
                    Message = "Department can not be created";
                    TempData["Message"] = Message;
                    
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
            #region AutoMapper
            var DepartmentViewModel = mapper.Map<DepartmentDetailsToReturnDto, DepartmentViewModel>(department);
            return View(DepartmentViewModel);
            #endregion

            #region Mapping Manually
            //return View(new DepartmentViewModel() { 
            //Code = department.Code,
            //    CreationDate = department.CreationDate,
            //    Description = department.Description,
            //    Name = department.Name
            //}); 
            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,DepartmentViewModel departmentS)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentS);
            }
            var Message = "";
            try {
                #region Auto Mapper
                var DepartmentUpdate = mapper.Map<DepartmentViewModel, DepartmentToUpdateDto>(departmentS);
                DepartmentUpdate.Id=id;
                var UpdateDepartment = DepartmentUpdate; 
                #endregion

                #region Manually Mapping
                //var UpdateDepartment = new DepartmentToUpdateDto()
                //{
                //    Id = id,
                //    Name = departmentS.Name,
                //    Code = departmentS.Code,
                //    CreationDate = departmentS.CreationDate,
                //    Description = departmentS.Description
                //}; 
                #endregion
                var department = service.UpdateDepartment(UpdateDepartment);
                if (department > 0)
                {
                    TempData["Message"] = "Department is Updated";
                    return RedirectToAction("Index");
                }
                else
                {
                    Message = "Department can not be updated";
                    TempData["Message"] = Message;
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
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {

            var result = service.DeleteDepartment(id);
            var Message = "";
            try {
                if (result > 0)
                {
                    TempData["Message"] = "Department is Deleted";
                    return RedirectToAction("Index");
                   
                }
                else
                {
                  Message = "Department can not be deleted";
                    TempData["Message"] = Message;
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
