﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models;
using Demo.DAL.Models.Departments;

namespace Demo.DAL.presistance.Repositories.Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IQueryable<T> GetAllQuery();
        IEnumerable<T> GetAll(bool AsNoTracing = true);

        T? GetById(int id);

        int AddDepartment(T department);

        int UpdateDepartment(T  department);

        int DeleteDepartment(T department);

    }
}
