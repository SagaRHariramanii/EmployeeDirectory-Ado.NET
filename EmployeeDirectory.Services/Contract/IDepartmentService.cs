namespace EmployeeDirectory.Services.Contract
{
    public interface IDepartmentService
    {
        int GetDepartmentId(string name);
        List<string> GetDepartmentList();
    }
}