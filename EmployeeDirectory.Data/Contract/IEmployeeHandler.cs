using EmployeeDirectory.Models;

namespace EmployeeDirectory.Data.Contract
{
    public interface IEmployeeHandler
    {
        void AddData(Employee employee);
        List<Employee> GetData();
        void Update<T>(string empId, Enum fieldName, T fieldInputData);
        void Delete(string empId);
    }
}