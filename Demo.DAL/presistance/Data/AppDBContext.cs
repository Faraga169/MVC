using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Departments;
using Demo.DAL.Models.Employees;
using Demo.DAL.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.presistance.Data
{
    //ApplicationUser:IdentityUser
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
       
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); ;
          
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=DemoDB;Trusted_Connection=True;TrustServerCertificate=True");
           
        }

        public DbSet<Department>  Department { get; set; }
        public DbSet<Employees> Employee { get; set; }

        // Identityuser,Identity Roles

        //public DbSet<IdentityUser> Users { get; set; }

        //public DbSet<IdentityRole> Roles { get; set; }

      
    }
}
