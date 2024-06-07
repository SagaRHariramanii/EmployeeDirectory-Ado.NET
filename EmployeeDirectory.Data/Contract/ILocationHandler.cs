using EmployeeDirectory.Models.Models;

namespace EmployeeDirectory.Data.Contract
{
    public interface ILocationHandler
    {
        List<Location> GetData();
    }
}