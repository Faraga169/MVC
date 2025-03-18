using AutoMapper;
using Demo.BLL.DTOS.Department;
using Demo.PL.ViewModels.Departments;

namespace Demo.PL.Mapping.profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region Employee modules

            #endregion

            #region Department modules
            CreateMap<DepartmentViewModel, DepartmentToCreateDto>();
            CreateMap<DepartmentDetailsToReturnDto, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, DepartmentToUpdateDto>();
            #endregion
        }


    }
}
