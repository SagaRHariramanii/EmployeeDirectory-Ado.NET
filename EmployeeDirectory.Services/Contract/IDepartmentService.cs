namespace EmployeeDirectory.Services.Contract
{
    public interface IDepartmentService
    {
        List<string> GetDepartments();
        string? GetDepartmentName(int id);
    }
}