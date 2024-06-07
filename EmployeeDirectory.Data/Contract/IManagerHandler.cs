using EmployeeDirectory.Models.Models;

namespace EmployeeDirectory.Data.Contract
{
    public interface IManagerHandler
    {
        List<Manager> GetData();
    }
}