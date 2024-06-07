using EmployeeDirectory.Models;

namespace EmployeeDirectory.Data.Contract
{
    public interface IRoleHandler
    {
        void AddData(Role role);
        List<Role> GetData();
        void Update(string roleId, string fieldName, string fieldInputData);
        void Delete(string roleId);
    }
}