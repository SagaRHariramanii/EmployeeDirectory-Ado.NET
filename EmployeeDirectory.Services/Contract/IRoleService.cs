using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services.Contract
{
    public interface IRoleService
    {
        void AddRole(Role role);
        Role? GetRoleInformation(string id);
        string? GetRoleId(string roleName, string location, string department);
        int GetRoleCount();
        Role GetRoleDataById(string roleId);
        List<Role> GetRoleDataList();

    }
}
