using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.presistance.Data
{
    public class AppDBContext:DbContext
    {
       
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); ;
          
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=DemoDB;Trusted_Connection=True;TrustServerCertificate=True");
           
        }

        public DbSet<Department>  Department { get; set; }
    }
}
