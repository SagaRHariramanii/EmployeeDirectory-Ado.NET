using EmployeeDirectory.Common;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Controller.Contract
{
    public interface IRoleController
    {
        void Add(Role role);
        Role? GetDataById(string roleId);
        int GetRoleCount();
        ValidationResult ValidateDepartment(string department);
        ValidationResult ValidateLocation(string location);
        ValidationResult ValidateRoleName(string roleName);
        string? GetRoleId(string roleName, string location, string department);
        List<Role> GetRoles();
        List<string> GetDepartments();
        List<string> GetLocations();

    }
}