using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services.Contract;

namespace EmployeeDirectory.Services.Services
{
    public class LocationService : ILocationService
    {
        ILocationHandler _locationHandler;
        public LocationService(ILocationHandler locationHandler)
        {
            this._locationHandler = locationHandler;
        }
        public List<string> GetLocationList()
        {
            List<string> department = [];
            List<Location> managerList = _locationHandler.GetData();
            for (int i = 0; i < managerList.Count; i++)
            {
                department.Add(managerList[i].Name);
            }
            return department;

        }
        public int GetLocationId(string name)
        {
            List<Location> locationList = _locationHandler.GetData();
            Location location = (from manager in locationList where (manager.Name).Equals(name) select manager).FirstOrDefault()!;
            return location.ID;
        }
    }
}
