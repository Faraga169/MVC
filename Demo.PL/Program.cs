using Demo.BLL.Common.Services.AttachmentService;
using Demo.BLL.Services.Department;
using Demo.BLL.Services.Employee;
using Demo.DAL.presistance.Data;
using Demo.DAL.presistance.Repositories.Departments;
using Demo.DAL.presistance.Repositories.Employees;
using Demo.DAL.presistance.Repositories.Generic;
using Demo.DAL.presistance.UnitofWork;
using Demo.PL.Controllers;
using Demo.PL.Mapping.profiles;
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

            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id:int?}");

            app.Run();
        }
    }
}
