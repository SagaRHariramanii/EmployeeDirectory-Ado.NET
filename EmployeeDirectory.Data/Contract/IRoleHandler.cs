using EmployeeDirectory.Models;

namespace EmployeeDirectory.Data.Contract
{
    public interface IRoleHandler
    {
        void AddRole(Role role);
        List<Role> GetData();
        Role? GetRoleById(string roleId);
        int GetRoleCount();
        string? GetRoleId(string roleName, string location, string department);
        void Update(string roleId, string fieldName, string fieldInputData);
        void Delete(string roleId);
    }
}