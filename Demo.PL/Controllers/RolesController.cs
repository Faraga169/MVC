using Demo.DAL.Models.Identity;
using Demo.PL.ViewModels;
using Demo.PL.ViewModels.Roles;
using Demo.PL.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        // GetAll , Get , Update , Delete
        // Index  , Details, Update , Delete

        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            //Roles
            var rolesQuery =roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue))
            {   // Search value =Mariam
                rolesQuery = rolesQuery.Where(R => R.Name.ToLower().Contains(SearchValue.ToLower()));
            }
            var rolesList = await rolesQuery.Select(R => new RoleViewModel()
            {
             Id = R.Id,
             Name=R.Name
            }).ToListAsync();
            return View(rolesList);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleView) {
            
            if (ModelState.IsValid) { 
            var role = new IdentityRole() { 
            Name=roleView.Name
            };
                var result=await roleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(roleView);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
                return BadRequest();

            var result = await roleManager.FindByIdAsync(id);
            if (result is null)
                return NotFound();
            var roleViewModel = new RoleViewModel()
            {
                Id = result.Id,
                Name=result.Name
            };
            return View(roleViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var users = await userManager.Users.ToListAsync();

            if (id is null)
                return BadRequest();

            var result = await roleManager.FindByIdAsync(id);

            if (result is null)
                return NotFound();

            var roleViewModel = new RoleViewModel()
            {
                Id = result.Id,
                Name = result.Name,
                Users = users.Select(user => new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = userManager.IsInRoleAsync(user, result.Name).Result
                }).ToList() // 🔹 Ensure the collection is properly materialized
            }; // 🔹 Missing semicolon added here ✅

            return View(roleViewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]// Action Filter
        public async Task<IActionResult> Edit(string id, RoleViewModel role)
        {
            if (id is null)
                return BadRequest();

            var roleId = await roleManager.FindByIdAsync(id);
            if (roleId is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                // ✅ Update the ApplicationUser object with values from UserViewModel
              roleId.Id= role.Id;
              roleId.Name = role.Name;

                var result = await roleManager.UpdateAsync(roleId);
                foreach (var userRole in role.Users) {
                    var user =await userManager.FindByIdAsync(userRole.UserId);
                    if (user is not null) {
                        if (userRole.IsSelected && !(await userManager.IsInRoleAsync(user,roleId.Name))) {
                            await userManager.AddToRoleAsync(user, roleId.Name);
                        }

                        else if (!userRole.IsSelected && await userManager.IsInRoleAsync(user,roleId.Name)) { 
                                await userManager.RemoveFromRoleAsync(user, roleId.Name);
                        }

                    }

                }

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(role);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
                return BadRequest();
            var result = await roleManager.FindByIdAsync(id);
            if (result is null)
                return NotFound();
            var roleViewModel = new RoleViewModel()
            {
               Id= result.Id,
               Name=result.Name
            };
            return View(roleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]// Action Filter
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var result = await roleManager.FindByIdAsync(id);
            if (result is not null)
                await roleManager.DeleteAsync(result);
            return RedirectToAction("Index");
        }
    }

}


