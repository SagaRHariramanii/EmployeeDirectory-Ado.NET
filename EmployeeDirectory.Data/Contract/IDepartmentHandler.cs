using EmployeeDirectory.Models.Models;

namespace EmployeeDirectory.Data.Contract
{
    public interface IDepartmentHandler
    {
        List<Department> GetData();
        string? GetDepartmentNameById(int id);
    }
}