using Demo.BLL.Common.Services.AttachmentService;
using Demo.BLL.Services.Department;
using Demo.BLL.Services.Employee;
using Demo.DAL.Models.Identity;
using Demo.DAL.presistance.Data;
using Demo.DAL.presistance.Repositories.Departments;
using Demo.DAL.presistance.Repositories.Employees;
using Demo.DAL.presistance.Repositories.Generic;
using Demo.DAL.presistance.UnitofWork;
using Demo.PL.Controllers;
using Demo.PL.Mapping.profiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDBContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
            });  // Add DI for AppDbContext

            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Add DI for DepartmentRepository
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            //builder.Services.AddScoped<IGenericRepository<T>,GenericRepository<T>>();
            builder.Services.AddScoped<IDepartmentService,DepartmentService>();  // Add DI for Department Service
            builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IUnitofwork, UnitofWork>();
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            //builder.Services.AddScoped<UserManager<ApplicationUser>>();
            //builder.Services.AddScoped<RoleManager<ApplicationUser>>();
            //builder.Services.AddScoped<SignInManager<ApplicationUser>>();

            // signature for Methods
            // Repository ====> stores
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) => {
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders(); // passwordsignInAsync Depend on AddDefaultTokenProvider service

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                option => {
                    option.LoginPath = "/Account/Login";
                    option.AccessDeniedPath = "/Home/Error";
                    option.LogoutPath = "/Account/Login";
                }


                );
            
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id:int?}");

            app.Run();
        }
    }
}
