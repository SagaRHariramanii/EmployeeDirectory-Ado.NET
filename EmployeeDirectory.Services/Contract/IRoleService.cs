using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services.Contract
{
    public interface IRoleService
    {
        void AddRole(Role role);
        string? GetRoleId(string roleName, string location, string department);
        int GetRoleCount();
        Role? GetRoleById(string roleId);
        List<Role> GetRoles();

    }
}
