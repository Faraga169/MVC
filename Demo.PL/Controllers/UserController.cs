using Demo.BLL.DTOS.Employee;
using Demo.DAL.Models.Departments;
using Demo.DAL.Models.Identity;
using Demo.PL.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    [Authorize]
    // Primary Constructor
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // GetAll , Get , Update , Delete
        // Index  , Details, Update , Delete

        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            //Users
            var usersQuery=userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue)) {   // Search value =Mariam
                 usersQuery=usersQuery.Where(U=>U.Email.ToLower().Contains(SearchValue.ToLower()));
            }

            var usersList= await usersQuery.Select(U=>new UserViewModel() { 
           Id=U.Id,
           FirstName=U.FName,
           LastName=U.LName,
           Email=U.Email,
            }).ToListAsync();

            foreach (var user in usersList ) {
                  user.Roles=await userManager.GetRolesAsync(await userManager.FindByIdAsync(user.Id));
            }

            return View(usersList);
        }


        [HttpGet]
        public async Task<IActionResult>Details(string id)
        {
            if (id is null)
                return BadRequest();

            var result = await userManager.FindByIdAsync(id);
            if (result is null)
                return NotFound();
            var userViewModel = new UserViewModel()
            {
                Id = result.Id,
                FirstName = result.FName,
                LastName = result.LName,
                Email = result.Email,
                Roles =  userManager.GetRolesAsync(result).Result //Synchronse
            };
            return View(userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult>Edit(string id)
        {
                if (id is null)
                    return BadRequest();
             var result = await userManager.FindByIdAsync(id);
                if (result is null)
                    return NotFound();
                var userViewModel = new UserViewModel()
                {
                    Id = result.Id,
                    FirstName = result.FName,
                    LastName = result.LName,
                    Email = result.Email,
                    Roles = await userManager.GetRolesAsync(result) 
                };
                return View(userViewModel);
            }
        

        [HttpPost]
        [ValidateAntiForgeryToken]// Action Filter
        public async Task<IActionResult> Edit(string id, UserViewModel user)
        {
            if (id is null)
                return BadRequest();

            var userId = await userManager.FindByIdAsync(id);
            if (userId is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                // ✅ Update the ApplicationUser object with values from UserViewModel
                userId.FName = user.FirstName;
                userId.LName = user.LastName;
                userId.Email = user.Email;

                var result = await userManager.UpdateAsync(userId);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }



        [HttpGet]
        public async Task<IActionResult>Delete(string id)
        {
            if (id is null)
                return BadRequest();
            var result = await userManager.FindByIdAsync(id);
            if (result is null)
                return NotFound();
            var userViewModel = new UserViewModel()
            {
                FirstName = result.FName,
                LastName = result.LName,
                Email = result.Email,
                Id = result.Id
            };
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]// Action Filter
        public async Task<IActionResult>ConfirmDelete(string id)
        {
            var result = await userManager.FindByIdAsync(id);
            if (result is not null)
                await userManager.DeleteAsync(result);
            return RedirectToAction("Index");
        }
    }

}

