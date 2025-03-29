using System.Security.Policy;
using Demo.BLL.Common.Services.EmailServices;
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
        private readonly IEmailSettings emailSettings;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IEmailSettings emailSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSettings = emailSettings;
        }

        // Register , Login , Sign Out

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]// Action Filter
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {

                var User = new ApplicationUser()
                {
                    UserName = registerViewModel.Email.Split('@')[0],
                    Email = registerViewModel.Email,
                    FName=registerViewModel.FName,
                    LName=registerViewModel.LName,
                    IsAgree=registerViewModel.IsAgree,
                };
                //Interfaces and Classes [Signature for methods and methods itself in classes]
                var result = await userManager.CreateAsync(User, registerViewModel.Password);
                

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
        [ValidateAntiForgeryToken]// Action Filter
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


        [HttpGet]
        public IActionResult ForgetPassword() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]// Action Filter
        public async Task<IActionResult> SendResetPasswordURL(ForgetPasswordModelView forgetPassword) {
            if (ModelState.IsValid) {
                var user = await userManager.FindByEmailAsync(forgetPassword.Email);
                if (user is not null)
                {
                    var token= await userManager.GeneratePasswordResetTokenAsync(user);
                    // Account/ResetPassword?email= mariam.gmail.com
                    var url = Url.Action("ResetPassword","Account",new {Email=forgetPassword.Email,token=token },Request.Scheme);
                    // TO , Subject , Body , Email =====> {To , Subject , Body}
                    var Email = new Email() {
                        To = forgetPassword.Email,
                        Subject = "Reset Your Password",
                        Body = url//Url;
                    };
                    // send email
                    emailSettings.SendEmail(Email);
                    return RedirectToAction("CheckYourInbox");

                }
                else {
                    ModelState.AddModelError(string.Empty, "This Email not Register once");
                }
            }
            return View(forgetPassword);
        }


        [HttpGet]
        public IActionResult CheckYourInbox() {
            return View();
        }



        [HttpGet]
        public IActionResult ResetPassword(string email,string token) {
            // pass email , token
            TempData["Email"] = email;
            TempData["token"] = token;            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]// Action Filter
        public async Task<IActionResult> ResetPassword(ResetPasswordModelView resetPassword) {
            if (ModelState.IsValid) {
                
                var email = TempData["Email"] as string;
                var token = TempData["token"] as string;
                var user=await userManager.FindByEmailAsync(email);
                if (user is not null) { 
                var result= await userManager.ResetPasswordAsync(user, token,resetPassword.Password);

                    if (result.Succeeded) {
                        return RedirectToAction("Login");
                    }
                }

                ModelState.AddModelError(string.Empty, "Email is Invalid");
            }
            ModelState.AddModelError(string.Empty, "Invalid Operations Please Try Again");
            return View(resetPassword);
        }
    }
}
