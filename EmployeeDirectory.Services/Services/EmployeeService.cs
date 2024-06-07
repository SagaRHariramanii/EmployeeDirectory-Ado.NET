using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models;
using EmployeeDirectory.Services.Contract;

namespace EmployeeDirectory.Services
{

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeHandler _employeeHandler;
        public EmployeeService(IEmployeeHandler employeeHandler)
        {
            this._employeeHandler = employeeHandler;
        }
        
        public void AddEmployee(Employee emp)
        {
            _employeeHandler.AddData(emp);
        }
        public void EditEmployee <T>(string empId, Enum fieldName ,T fieldInputData)
        {
            _employeeHandler.Update(empId, fieldName, fieldInputData);
        }
        public void DeleteEmployee(string employeeId)
        {
            _employeeHandler.Delete(employeeId);
        }
        public Employee? GetEmployeeDataById(string empId)
        {
            List<Employee> employeeDataList =_employeeHandler.GetData();
            Employee employeeData = (from emp in employeeDataList where emp.EmpId == empId select emp).FirstOrDefault()!;
            return employeeData;
        }
        public List<Employee> GetEmployeeDataList()
        {
            List<Employee> employeeList = _employeeHandler.GetData();
            return employeeList;

        }
    }
}
