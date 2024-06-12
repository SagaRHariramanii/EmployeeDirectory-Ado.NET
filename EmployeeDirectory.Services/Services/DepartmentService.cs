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
        public List<string> GetDepartments()
        {
            List<string> department = [];
            List<Department> departments = _departmentHandler.GetData();
            for (int i = 0; i < departments.Count; i++)
            {
                department.Add(departments[i].Name);
            }
            return department;

        }
        public string? GetDepartmentName(int id)
        {
            return _departmentHandler.GetDepartmentNameById(id);
        }
    }
}
