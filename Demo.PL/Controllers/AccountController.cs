using Demo.DAL.Models.Identity;
using Demo.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // Register , Login , Sign Out

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {

                var User = new ApplicationUser()
                {
                    UserName = registerViewModel.Email.Split('@')[0],
                    Email = registerViewModel.Email,
                    PasswordHash = registerViewModel.Password,
                    FName=registerViewModel.FName,
                    LName=registerViewModel.LName,
                    IsAgree=registerViewModel.IsAgree,
                };
                //Interfaces and Classes [Signature for methods and methods itself in classes]
              var result=  await userManager.CreateAsync(User,registerViewModel.Password);

                if (result.Succeeded)
                    return RedirectToAction("Login");

                else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
              
                }

                
            }
            return View(registerViewModel);   // Model State is not valid or result not succeded

        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid) {


                var User = await userManager.FindByEmailAsync(loginViewModel.Email); // check user exist with same email or not

                if (User is not null)
                {
                 var flag=  await userManager.CheckPasswordAsync(User,loginViewModel.Password);
                    if (flag)
                    {  // check email is exist and password correct
                        // Token==> encrypted string
                        var result = await signInManager.PasswordSignInAsync(User, loginViewModel.Password, loginViewModel.RememberMe, false);

                        if (result.Succeeded)
                            return RedirectToAction("Index","Home");
                      
                    }

                    else {  // email is exist but password is wrong
                        ModelState.AddModelError(string.Empty, "Password is not Found");
                    }
                }

                else {
                    ModelState.AddModelError(string.Empty, "Email is not Found");
                }
               
            }
            return View(loginViewModel);
        }


        [HttpGet]
        public new async Task<IActionResult> SignOut() {
            await signInManager.SignOutAsync();  // To delete Token When sign out
            return RedirectToAction("Login");
        }

    }
}
