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
        public List<string> GetManagers()
        {
            List<string> manager = [];
            List<Manager> managers = _managerHandler.GetData();
            for (int i = 0; i < manager.Count; i++)
            {
                manager.Add(managers[i].Name);
            }
            return manager;

        }
        public int GetManagerId(string name)
        {
            List<Manager> managers = _managerHandler.GetData();
            Manager manager = (from mng in managers where (mng.Name).Equals(name) select mng).FirstOrDefault()!;
            return manager.ID;
        }
    }
}
