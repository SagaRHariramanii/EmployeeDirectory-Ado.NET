using EmployeeDirectory.Models.Models;

namespace EmployeeDirectory.Data.Contract
{
    public interface IProjectHandler
    {
        List<Project> GetData();
        string? GetProjectNameById(int id);
    }
}