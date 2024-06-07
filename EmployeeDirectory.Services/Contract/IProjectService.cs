namespace EmployeeDirectory.Services.Contract
{
    public interface IProjectService
    {
        int GetProjectId(string name);
        List<string> GetProjects();
    }
}