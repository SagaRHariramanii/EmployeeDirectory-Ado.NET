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
            _roleHandler.AddData(role);
        }
        public Role? GetRoleInformation(string roleId)
        {
            List<Role> roleDataList = _roleHandler.GetData();
            Role roleObj = new();
            foreach (Role role in roleDataList)
            {
                if (role.ID == roleId)
                {
                    roleObj.Name = role.Name;
                    roleObj.Location = role.Location;
                    roleObj.Department = role.Department;
                    return roleObj;
                }
            }
            return null;
        }
        public string? GetRoleId(string roleName, string location, string department)
        {
            List<Role> roleDataList = _roleHandler.GetData();
            string roleId = (from role in roleDataList where role.Name == roleName && role.Location == location && role.Department == department select role.ID).FirstOrDefault()!;
            return roleId;
        }
        public int GetRoleCount()
        {
            List<Role> roleDataList = _roleHandler.GetData();
            return roleDataList.Count;
        }
        public Role GetRoleDataById(string roleId)
        {
            List<Role> roleDataList = _roleHandler.GetData();
            Role roleData= (from role in roleDataList where role.ID == roleId select role).FirstOrDefault()!;
            return roleData;

        }
        
        public List<Role> GetRoleDataList()
        {
            return _roleHandler.GetData();
        }


    }
}
