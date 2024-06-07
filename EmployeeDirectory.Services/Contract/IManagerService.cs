namespace EmployeeDirectory.Services.Contract
{
    public interface IManagerService
    {
        int GetManagerId(string name);
        List<string> GetManagers();
    }
}