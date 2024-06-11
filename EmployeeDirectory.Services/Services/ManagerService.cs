using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services.Contract;

namespace EmployeeDirectory.Services.Services
{
    public class ManagerService : IManagerService
    {
        readonly IManagerHandler _managerHandler;
        public ManagerService(IManagerHandler managerHandler)
        {
            this._managerHandler = managerHandler;
        }
        public List<Manager> GetManagers()
        {
            List<Manager> managers = _managerHandler.GetManagers();
            return managers;

        }
        public string? GetManagerName(int id)
        {
            return _managerHandler.GetMangerNameById(id);
        }
    }
}
