namespace EmployeeDirectory.Services.Contract
{
    public interface ILocationService
    {
        List<string> GetLocations();
        string? GetLocationName(int id);
    }
}