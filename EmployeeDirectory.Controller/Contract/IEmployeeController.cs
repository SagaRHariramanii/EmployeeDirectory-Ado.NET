using EmployeeDirectory.Common;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;

namespace EmployeeDirectory.Controller.Contract
{
    public interface IEmployeeController
    {
        ValidationResult ValidateDate(string dob);
        ValidationResult ValidateEmail(string email);
        ValidationResult ValidateEmployeeId(string employeeId);
        ValidationResult ValidateFirstName(string firstName);
        ValidationResult ValidateLastName(string lastName);
        ValidationResult ValidatePhoneNumber(string phone);
        Employee GetDataById(string empId);
        void Add(Employee employee);
        List<Employee> GetEmployeeList();
        void Delete(string employeeId);
        void Edit<T>(string empId, Enum employeeField, T employeeFieldInput);
        List<string> GetMangersList();
        List<string> GetProjectList();
        int GetManagerId(string name);
        int GetProjectId(string name);
    }
}