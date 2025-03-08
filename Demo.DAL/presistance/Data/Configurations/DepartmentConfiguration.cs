using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DAL.presistance.Data.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            
            builder.Property(d => d.Id).UseIdentityColumn(10,10);
            builder.Property(d => d.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(d => d.Code).HasColumnType("nvarchar(30)").IsRequired();
            builder.Property(d=>d.CreatedOn).HasDefaultValueSql("getdate()");
            builder.Property(d => d.LastModifiedOn).HasDefaultValueSql("getdate()");
        }
    }
}
