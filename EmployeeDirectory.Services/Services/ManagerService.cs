using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services.Contract;

namespace EmployeeDirectory.Services.Services
{
    public class ManagerService : IManagerService
    {
        readonly IManagerHandler ManagerHandler;
        public ManagerService(IManagerHandler managerHandler)
        {
            this.ManagerHandler = managerHandler;
        }
        public List<string> GetManagers()
        {
            List<string> Manager = [];
            List<Manager> ManagerList = ManagerHandler.GetData();
            for (int i = 0; i < ManagerList.Count; i++)
            {
                Manager.Add(ManagerList[i].Name);
            }
            return Manager;

        }
        public int GetManagerId(string name)
        {
            List<Manager> ManagerList = ManagerHandler.GetData();
            Manager managerList = (from manager in ManagerList where (manager.Name).Equals(name) select manager).FirstOrDefault()!;
            return managerList.ID;
        }
    }
}
