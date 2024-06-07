namespace EmployeeDirectory.Services.Contract
{
    public interface ILocationService
    {
        int GetLocationId(string name);
        List<string> GetLocationList();
    }
}