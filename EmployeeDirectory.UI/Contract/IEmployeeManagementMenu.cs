using EmployeeDirectory.Models;

namespace EmployeeDirectory.UI.Contract
{
    public interface IEmployeeManagementMenu
    {
        void DisplayEmployeeData(Employee employeeData);
        void EmployeeManagmentMenuOptions();
        string GetDepartment(string location);
        string GetJobTitle(string location, string department);
        string GetLocation();
        void OptionAddEmployee();
        void OptionDeleteParticularEmployeeData();
        void OptionDisplayAllEmployeeData();
        void OptionDisplayEmployeeById();
        void OptionEditEmployee(Employee employeeData, string employeeID);
    }
}