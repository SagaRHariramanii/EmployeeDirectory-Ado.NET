using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services.Contract;

namespace EmployeeDirectory.Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        IDepartmentHandler _departmentHandler;
        public DepartmentService(IDepartmentHandler departmentHandler)
        {
            this._departmentHandler = departmentHandler;
        }
        public List<string> GetDepartmentList()
        {
            List<string> department = [];
            List<Department> managerList = _departmentHandler.GetData();
            for (int i = 0; i < managerList.Count; i++)
            {
                department.Add(managerList[i].Name);
            }
            return department;

        }
        public int GetDepartmentId(string name)
        {
            List<Department> departmentList = _departmentHandler.GetData();
            Department department = (from manager in departmentList where (manager.Name).Equals(name) select manager).FirstOrDefault()!;
            return department.ID;
        }
    }
}
