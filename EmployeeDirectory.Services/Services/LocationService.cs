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
        public List<string> GetLocations()
        {
            List<string> location = [];
            List<Location> locations = _locationHandler.GetData();
            for (int i = 0; i < locations.Count; i++)
            {
                location.Add(locations[i].Name);
            }
            return location;

        }
        public string? GetLocationName(int id)
        {
            return _locationHandler.GetLocationNameById(id);
        }
    }
}
