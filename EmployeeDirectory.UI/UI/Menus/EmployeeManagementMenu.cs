using EmployeeDirectory.Common;
using EmployeeDirectory.Controller;
using EmployeeDirectory.Controller.Contract;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services;
using EmployeeDirectory.UI.Contract;
using static EmployeeDirectory.Models.Models.Enums;

namespace EmployeeDirectory.UI.Menus
{
    public class EmployeeManagementMenu:IEmployeeManagementMenu
    {
        IEmployeeController _employeeController;
        IRoleController _roleController;
        
        public EmployeeManagementMenu(IEmployeeController employeeController,IRoleController roleController)
        {
            this._employeeController = employeeController;
            this._roleController = roleController;
        }

        public void EmployeeManagmentMenuOptions()
        {
            Console.WriteLine("Employee Management Menu");
            Console.WriteLine("1. Add new employee");
            Console.WriteLine("2. Display all Employees");
            Console.WriteLine("3. Display one Employee");
            Console.WriteLine("4. Edit employee Detail");
            Console.WriteLine("5. Delete employee Data");
            Console.WriteLine("6. Go Back");
            Console.Write("Choice = ");
            int employeeMangementChoice = Parser.ParseToInt(Console.ReadLine()!);
            if (employeeMangementChoice==-1)
            {
                Console.WriteLine("Invalid Choice Select Again");
                EmployeeManagmentMenuOptions();
            }
            else
            {
                switch (employeeMangementChoice)
                {
                    case 1:
                        OptionAddEmployee();
                        break;
                    case 2:
                        OptionDisplayAllEmployeeData();
                        break;
                    case 3:
                        OptionDisplayEmployeeById();
                        break;
                    case 4:
                        Console.WriteLine("--------------------- Edit Particular Employee Data ---------------------");
                        Console.Write("Enter Employee No. = ");
                        string employeeID = Console.ReadLine()!;
                        Employee employeeData = _employeeController.GetDataById(employeeID)!;
                        if (!(employeeData == null || employeeData.IsDeleted))
                        {
                            DisplayEmployeeData(employeeData);
                            OptionEditEmployee(employeeData, employeeID);
                        }
                        else
                        {
                            Console.WriteLine("Employee with Employee id " + employeeID + " Not Found");
                        }
                        break;
                    case 5:
                        OptionDeleteParticularEmployee();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;

                }
            }
        }
        public  string GetEmployeeId()
        {
            Console.Write("Enter Employee No. : ");
            string employeeId = (Console.ReadLine()!);
            ValidationResult employeeIdValidator = _employeeController.ValidateEmployeeId(employeeId);
            if (!employeeIdValidator.IsValid)
            {
                Console.WriteLine(employeeIdValidator.Message);
                string empId = GetEmployeeId();
                return empId;
            }
            else
            {
                return employeeId;
            }
        }
        public  string GetFirstName()
        {
            Console.Write("Enter First Name : ");
            string firstName = (Console.ReadLine()!);
            ValidationResult firstNameValidator = _employeeController.ValidateFirstName(firstName);
            if (!firstNameValidator.IsValid)
            {
                Console.WriteLine(firstNameValidator.Message);
                string empFirstName = GetFirstName();
                return empFirstName;
            }
            else
            {
                return firstName;
            }
        }
        public  string GetLastName()
        {
            Console.Write("Enter Last Name : ");
            string lastName = (Console.ReadLine()!);
            ValidationResult lastNameValidator = _employeeController.ValidateLastName(lastName);
            if (!lastNameValidator.IsValid)
            {
                Console.WriteLine(lastNameValidator.Message);
                string empLastName = GetLastName();
                return empLastName;
            }
            else
            {
                return lastName;
            }
        }
        public  DateTime GetDob()
        {
            Console.Write("Enter Date of Birth : ");
            string Dob = (Console.ReadLine()!);
            ValidationResult dobValidator = _employeeController.ValidateDate(Dob);
            if (!dobValidator.IsValid)
            {
                Console.WriteLine(dobValidator.Message);
                return GetDob();
            }
            else
            {
                return DateTime.Parse(Dob);
            }

        }
        public  string GetEmail()
        {
            Console.Write("Enter Email : ");
            string email = (Console.ReadLine()!);
            ValidationResult emailValidator = _employeeController.ValidateEmail(email);
            if (!emailValidator.IsValid)
            {
                Console.WriteLine(emailValidator.Message);
                string empEmail = GetEmail();
                return empEmail;
            }
            else
            {
                return email;
            }
        }
        public  string GetPhoneNumber()
        {
            Console.Write("Enter Mobile Number : ");
            string phoneNo = (Console.ReadLine()!);
            ValidationResult phoneNumberValidator = _employeeController.ValidatePhoneNumber(phoneNo);
            if (!phoneNumberValidator.IsValid)
            {
                Console.WriteLine(phoneNumberValidator.Message);
                string empPhoneNo = GetPhoneNumber();
                return empPhoneNo;
            }
            else
            {
                return phoneNo;
            }
        }
        public  DateTime GetJoiningDate()
        {
            Console.Write("Enter Joining Date : ");
            string joiningDate = (Console.ReadLine()!);
            ValidationResult joiningDateValidator = _employeeController.ValidateDate(joiningDate);
            if (joiningDateValidator.IsValid)
            {
                return DateTime.Parse(joiningDate);
            }
            else
            {
                Console.WriteLine(joiningDateValidator.Message);
                return GetJoiningDate();
            }

        }
        public string GetLocation()
        {
            int i = 1;
            List<string> locations =_roleController.GetLocations();
           
            Console.WriteLine("--------------------- Locations ---------------------");
            foreach (string loc in locations)
            {
                Console.WriteLine(i + ". " + loc);
                i++;
            }
            Console.Write("\nEnter the location : ");
            int selectedOption = int.Parse(Console.ReadLine()!);
            if (selectedOption > (locations.Count))
            {
                Console.WriteLine("Invalid Choice..");
                string empLocation = GetLocation();
                return empLocation;
            }
            else
            {
                string location = locations[selectedOption - 1];
                return location;
            }
        }
        public string GetDepartment(string location)
        {
            string selectedLocation = location;
            int i = 1;
            List<string> departments = [];
            for (int j = 1; j <= _roleController.GetRoleCount(); j++)
            {
                if (selectedLocation == _roleController.GetDataById("R" + j)!.Location)
                {
                    departments.Add(_roleController.GetDataById("R" + j)!.Department);
                }
            }
            string[] uniqueDepartments = departments.Distinct().ToArray();
            Console.WriteLine("--------------------- Departments ---------------------");
            foreach (string dep in uniqueDepartments)
            {
                Console.WriteLine(i + ". " + dep);
                i++;
            }
            Console.Write("\nEnter the Department : ");
            int departmentChoice = int.Parse(Console.ReadLine()!);
            if (departmentChoice > (uniqueDepartments.Length + 1))
            {
                Console.WriteLine("Invalid Choice..");
                string empDepartment = GetDepartment(selectedLocation);
                return empDepartment;
            }
            else
            {
                string department = uniqueDepartments[departmentChoice - 1];
                return department;
            }
        }
        public string GetJobTitle(string location, string department)
        {
            string selectedLocation = location;
            string selectedDepartment = department;
            List<string> jobTitles = [];
            int i = 1;
            for (int j = 1; j <= _roleController.GetRoleCount(); j++)
            {
                if (selectedLocation == _roleController.GetDataById("R"+j)!.Location)
                {
                    if (selectedDepartment == _roleController.GetDataById("R"+j)!.Department)
                    {
                        jobTitles.Add(_roleController.GetDataById("R"+j)!.Name);
                    }
                }
            }
            string[] uniqueJobTitles = jobTitles.Distinct().ToArray();
            Console.WriteLine("--------------------- Job Titles ---------------------");
            foreach (string jobtitle in uniqueJobTitles)
            {
                Console.WriteLine(i + ". " + jobtitle);
                i++;
            }

            Console.Write("\n Enter the Job Title : ");
            int jobTitleChoice = int.Parse(Console.ReadLine()!);
            if (jobTitleChoice > (uniqueJobTitles.Length + 1))
            {
                Console.WriteLine("Invalid Choice..");
                string empJobTitle = GetJobTitle(selectedLocation, selectedDepartment);
                return empJobTitle;
            }
            else
            {
                string jobTitle = uniqueJobTitles[jobTitleChoice - 1];
                return jobTitle;
            }
        }
        public string GetManager()
        {
            int i = 1;
            List<string> managerList = _employeeController.GetMangersList();
            foreach (string manager in managerList)
            {
                Console.WriteLine(i + ". " + manager);
                i++;
            }
            Console.Write("Enter the ManagerName : ");
            int managerChoice  = int.Parse(Console.ReadLine()!);
            if (managerChoice > (managerList.Count))
            {
                Console.WriteLine("Invalid Choice..");
                string manager = GetManager();
                return manager;
            }
            else
            {
                string manager = managerList[managerChoice - 1];
                return manager;
            }
        }
        public  string GetProject()
        {
            int i = 1;
            List<string> projectList = _employeeController.GetProjectList();
            foreach (string project in projectList)
            {
                Console.WriteLine(i + ". " + project);
                i++;
            }
            Console.Write("Enter the ProjectName : ");
            int projectChoice = int.Parse(Console.ReadLine()!);
            if (projectChoice > (projectList.Count))
            {
                Console.WriteLine("Invalid Choice..");
                string project = GetManager();
                return project;
            }
            else
            {
                string project = projectList[projectChoice - 1];
                return project;
            }
        }
        public void OptionDisplayEmployeeById()
        {
            Console.WriteLine("--------------------- Displaying a particular Employee Data ---------------------");
            Console.Write("Enter Employee No. : ");
            string employeeId = Console.ReadLine()!;
            Employee employeeData = _employeeController.GetDataById(employeeId)!;
            if (employeeData == null || employeeData.IsDeleted)
            {
                Console.WriteLine("Employee with Employee id " + employeeId + " Not Found");
            }
            else
            {
                Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                string header = string.Format("|{0,10}|{1,25}|{2,30}|{3,20}|{4,15}|{5,20}|{6,20}|{7,20}|", "EmpID", "Name", "Role", "Department", "Location", "Join Date", "Manager Name", "Project Name");
                Console.WriteLine(header);
                Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                Role roleDetail = _roleController.GetDataById(employeeData.RoleId)!;
                Console.WriteLine("|{0,10}|{1,25}|{2,30}|{3,20}|{4,15}|{5,20}|{6,20}|{7,20}|", employeeData.EmpId, employeeData.FirstName + " " + employeeData.LastName, roleDetail.Name, roleDetail.Department, roleDetail.Location, employeeData.JoiningDate, employeeData.ManagerName, employeeData.ProjectName);
                Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");

            }

        }
        public void OptionAddEmployee()
        {
            Employee emp = new();
            Console.WriteLine("--------------------- Add employee ---------------------");
            emp.EmpId = GetEmployeeId();
            emp.FirstName = GetFirstName();
            emp.LastName = GetLastName();
            emp.Dob = GetDob();
            emp.Email = GetEmail();
            emp.PhoneNo = GetPhoneNumber();
            emp.JoiningDate = GetJoiningDate();
            string location = GetLocation();
            string department = GetDepartment(location);
            string jobTitle = GetJobTitle(location, department);
            emp.ManagerName = GetManager();
            emp.ProjectName = GetProject();
            emp.RoleId = _roleController.GetRoleId(jobTitle,location, department)!;
            emp.IsDeleted = false;
            _employeeController.Add(emp);
            Console.WriteLine("Employee Added SuccessFully");
            Console.Write("Do you want to add more Employee (y/n): ");
            string addMoreChoice = Console.ReadLine()!.ToLower();
            if (addMoreChoice == "y")
            {
                OptionAddEmployee();
            }
            else
            {
                Environment.Exit(0);
            }
        }
        public void OptionDisplayAllEmployeeData()
        {
            Console.WriteLine("--------------------- Displaying all Employees Data ---------------------\n");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            string header = string.Format("|{0,10}|{1,25}|{2,30}|{3,20}|{4,15}|{5,20}|{6,20}|{7,20}|", "EmpID", "Name", "Role", "Department", "Location", "Join Date", "Manager Name", "Project Name");
            Console.WriteLine(header);
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            List<Employee> employeeDataList = _employeeController.GetEmployeeList();
            for (int i = 0; i < employeeDataList.Count; i++)
            {
                Employee empData = employeeDataList[i];
                if (empData.IsDeleted)
                {
                    continue;
                }
                else
                {
                    Role roleDetail = _roleController.GetDataById(empData.RoleId)!;
                    string employeeData = string.Format("|{0,10}|{1,25}|{2,30}|{3,20}|{4,15}|{5,20}|{6,20}|{7,20}|", empData.EmpId, empData.FirstName + " " + empData.LastName, roleDetail.Name, roleDetail.Department, roleDetail.Location, empData.JoiningDate, empData.ManagerName, empData.ProjectName);
                    Console.WriteLine(employeeData);
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                }
            }
            Console.WriteLine();
        }
        public void DisplayEmployeeData(Employee employeeData)
        {
            Console.WriteLine("1.  First Name: " + employeeData.FirstName);
            Console.WriteLine("2.  Last Name: " + employeeData.LastName);
            Console.WriteLine("3.  Date of Birth: " + employeeData.Dob);
            Console.WriteLine("4.  Email: " + employeeData.Email); 
            Console.WriteLine("5.  Phone number: " + employeeData.PhoneNo);
            Console.WriteLine("6.  Joining Date: " + employeeData.JoiningDate);
            Role roleDetail = _roleController.GetDataById(employeeData.RoleId)!;
            if (roleDetail != null)
            {
                Console.WriteLine("7.  Location: " + roleDetail.Location);
                Console.WriteLine("8.  Job Title: " + roleDetail.Name);
                Console.WriteLine("9.  Department: " + roleDetail.Department);
            }
            else
            {
                Console.WriteLine("7.  Location: " + "");
                Console.WriteLine("8.  Job Title: " + "");
                Console.WriteLine("9.  Department: " + "");
            }
            Console.WriteLine("10. Manager Name: " + employeeData.ManagerName);
            Console.WriteLine("11. Project Name: " + employeeData.ProjectName);
        }
        public void OptionEditEmployee(Employee employeeData, string employeeID)
        {
            Console.Write("Which Field you want to Change:");
            int choice = int.Parse(Console.ReadLine()!);
            switch (choice)
            {
                case 1:
                    string firstName = GetFirstName();
                    _employeeController.Edit(employeeID, EmployeeField.FirstName,firstName);
                    Console.WriteLine("First Name Changed SuccessFully");
                    break;
                case 2:
                    string lastName = GetLastName();
                    _employeeController.Edit(employeeID,EmployeeField.LastName, lastName);
                    Console.WriteLine("Last Name Changed SuccessFully");
                    break;
                case 3:
                    DateTime dob = GetDob();
                    _employeeController.Edit(employeeID,EmployeeField.Dob ,dob);
                    Console.WriteLine("Date Of Birth Changed SuccessFully");
                    break;
                case 4:
                    string email = GetEmail();
                    _employeeController.Edit(employeeID,EmployeeField.Email,email);
                    Console.WriteLine("Email Changed SuccessFully");
                    break;
                case 5:
                    string phoneNo = GetPhoneNumber();
                    _employeeController.Edit(employeeID,EmployeeField.PhoneNo ,phoneNo);
                    Console.WriteLine("Phone Number Changed SuccessFully");
                    break;
                case 6:
                    DateTime joiningDate = GetJoiningDate();
                    _employeeController.Edit(employeeID, EmployeeField.JoiningDate,joiningDate);
                    Console.WriteLine("Joining Date Changed SuccessFully");
                    break;
                case 7:
                    string changedLocation = GetLocation();
                    string changedDepartment = GetDepartment(changedLocation);
                    string changedJobTitle = GetJobTitle(changedLocation, changedDepartment);
                    string roleId = _roleController.GetRoleId(changedJobTitle,changedLocation, changedDepartment)!;
                    _employeeController.Edit(employeeID,EmployeeField.RoleId ,roleId);
                    Console.WriteLine("Location,Department,Job Title Changed SuccessFully");
                    break;
                case 8:
                    goto case 7;
                case 9:
                    goto case 7;
                case 10:
                    string manager = GetManager();
                    int managerId=_employeeController.GetManagerId(manager);
                    _employeeController.Edit(employeeID,EmployeeField.ManagerId, managerId);
                    Console.WriteLine("ManagerName Changed Successfully");
                    break;
                case 11:
                    string project = GetProject();
                    int projectId=_employeeController.GetProjectId(project);
                    _employeeController.Edit(employeeID,EmployeeField.ProjectId, projectId);
                    Console.WriteLine("ProjectName Changed Successfully");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            Console.Write("\nDo you want to add more Employee (y/n): ");
            string addMoreChoice = Console.ReadLine()!.ToLower();
            if (addMoreChoice == "y")
            {
                OptionEditEmployee(employeeData, employeeID);
            }
            else
            {
                Console.WriteLine("---------------------  Updated Data --------------------- ");
                Employee updatedDetail=_employeeController.GetDataById(employeeID);
                DisplayEmployeeData(updatedDetail);
                Environment.Exit(0);
            }
        }
        public void OptionDeleteParticularEmployee()
        {
            Console.WriteLine("--------------------- Delete a Particular Employee Data ---------------------");
            Console.Write("Enter the EmployeeId of Employee : ");
            string empId = Console.ReadLine()!;
            Employee employeeData = _employeeController.GetDataById(empId)!;
            if (employeeData == null || employeeData.IsDeleted)
            {
                Console.WriteLine("Employee with Employee Id " + empId + " Not Found!");
            }
            else
            {
                _employeeController.Delete(empId);
                Console.WriteLine("Employee with Employee Id " + empId + " Deleted SuccessFully!");
            }

        }
    }

}
