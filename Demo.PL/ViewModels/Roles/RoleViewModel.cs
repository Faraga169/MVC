using System.Runtime.InteropServices;

namespace Demo.PL.ViewModels.Roles
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string  Name { get; set; }

        public List<UserRoleViewModel> Users { get; set; } = new List<UserRoleViewModel>();


    }
}
