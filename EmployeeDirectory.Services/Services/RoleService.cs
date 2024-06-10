using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models;
using EmployeeDirectory.Services.Contract;
namespace EmployeeDirectory.Services
{
    public class RoleService : IRoleService
    {
        IRoleHandler _roleHandler;
        public RoleService(IRoleHandler roleHandler)
        {
            this._roleHandler = roleHandler;
        }
        public void AddRole(Role role)
        {
            _roleHandler.AddRole(role);
        }
        public string? GetRoleId(string roleName, string location, string department)
        {
            return _roleHandler.GetRoleId(roleName, location, department);
        }
        public int GetRoleCount()
        {
            return _roleHandler.GetRoleCount();
        }
        public Role? GetRoleById(string roleId)
        {
            return _roleHandler.GetRoleById(roleId);
        }
        public List<Role> GetRoles()
        {
            return _roleHandler.GetData();
        }


    }
}
