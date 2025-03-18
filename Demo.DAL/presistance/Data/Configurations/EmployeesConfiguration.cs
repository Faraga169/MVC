using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Common.Enum;
using Demo.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DAL.presistance.Data.Configurations
{
    internal class EmployeesConfiguration : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.Age).HasColumnType("int");
            builder.Property(e => e.Address).HasColumnType("varchar(100)");
            builder.Property(e => e.Salary).HasColumnType("decimal(8,2)");
            builder.Property(e => e.IsActive).HasColumnType("bit");
            builder.Property(e => e.PhoneNumber).HasColumnType("int");
            builder.Property(e => e.HiringDate).HasColumnType("date").HasDefaultValueSql("getDate()");
            builder.Property(e=>e.Gender).HasConversion(
                (gender)=>gender.ToString(),
                (gender)=>(Gender)Enum.Parse(typeof(Gender),gender)
                );
            builder.Property(e => e.EmployeeType).HasConversion(
                (employeeType) => employeeType.ToString(),
                (employeeType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), employeeType)
                );

            builder.Property(d => d.CreatedOn).HasDefaultValueSql("getdate()");
            builder.Property(d => d.LastModifiedOn).HasDefaultValueSql("getdate()");

            builder.HasOne(E => E.Department).WithMany(D => D.Employees).HasForeignKey(E => E.DepartmentId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
