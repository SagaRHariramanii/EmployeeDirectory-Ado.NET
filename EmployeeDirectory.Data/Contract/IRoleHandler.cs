using EmployeeDirectory.Models;

namespace EmployeeDirectory.Data.Contract
{
    public interface IRoleHandler
    {
        void AddRole(Role role);
        List<Role> GetData();
        Role? GetRoleById(int roleId);
        int GetRoleCount();
        int? GetRoleId(string roleName, string location, string department);
        void Update(string roleId, string fieldName, string fieldInputData);
        void Delete(int roleId);
    }
}