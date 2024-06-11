using EmployeeDirectory.Common;
using EmployeeDirectory.Controller.Contract;
using EmployeeDirectory.Models;
using EmployeeDirectory.UI.Contract;

namespace EmployeeDirectory.UI.Menus
{

    public class RoleManagmentMenu:IRoleManagmentMenu
    {
        IRoleController _roleController;
        public RoleManagmentMenu(IRoleController roleController)
        {
            _roleController = roleController;
        }

        public void RoleManagmentMenuOptions()
        {
            Console.WriteLine("1. Add new role");
            Console.WriteLine("2. Display all Roles");
            Console.WriteLine("3. Go Back");
            Console.Write("Choice = ");
            int roleManagmentChoice = Parser.ParseToInt(Console.ReadLine()!);
            if (roleManagmentChoice==-1)
            {
                Console.WriteLine("Invalid Choice Select Again");
                RoleManagmentMenuOptions();
            }
            else
            {
                switch (roleManagmentChoice)
                {
                    case 1:
                        OptionAddRole();
                        break;
                    case 2:
                        OptionDisplayAllRoles();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }

        }
        public string GetRoleName()
        {
            Console.Write("Enter the Role Name : ");
            string roleName = Console.ReadLine()!;
            ValidationResult RoleNameValidator = _roleController.ValidateRoleName(roleName);
            if (!RoleNameValidator.IsValid)
            {
                Console.WriteLine(RoleNameValidator.Message);
                string rName = GetRoleName();
                return rName;
            }
            else
            {
                return roleName;
            }
        }
        public  string GetLocation()
        {
            int i = 1;
            List<string> locations = _roleController.GetLocations();
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
        public  string GetDepartment()
        {
            int i = 1;
            List<string> departments = _roleController.GetDepartments();
            Console.WriteLine("--------------------- Departments ---------------------");
            foreach (string dep in departments)
            {
                Console.WriteLine(i + ". " + dep);
                i++;
            }
            Console.Write("\nEnter the Department : ");
            int selectedOption = int.Parse(Console.ReadLine()!);
            if (selectedOption > (departments.Count))
            {
                Console.WriteLine("Invalid Choice..");
                string empDepartment = GetDepartment();
                return empDepartment;
            }
            else
            {
                string department = departments[selectedOption - 1];
                return department;
            }
        }
        public  string GetRoleDescription()
        {
            Console.Write("Enter the Job Description : ");
            string roleDescription = Console.ReadLine()!;
            return roleDescription;
        }
        public void OptionAddRole()
        {
            Role role = new();
            Console.WriteLine("--------------------- Add Role ---------------------");
            role.Name = GetRoleName();
            role.Location=GetLocation();
            role.Department = GetDepartment();
            role.Description = GetRoleDescription();
            _roleController.Add(role);
            Console.WriteLine("Role Added SuccessFully");
            Console.Write("Do you want to add more Role (y/n): ");
            string addMoreChoice = Console.ReadLine()!.ToLower();
            if (addMoreChoice == "y")
            {
                OptionAddRole();
            }
            else
            {
                Environment.Exit(0);
            }
        }
        public void OptionDisplayAllRoles()
        {
            Console.WriteLine("RoleList");
            int countRoleObj = _roleController.GetRoleCount();
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            string header = string.Format("|{0,30}|{1,20}|{2,20}|{3,30}|", "Role Name", "Location", "Department", "Description");
            Console.WriteLine(header);
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            List<Role> roleDataList = _roleController.GetRoles();
            for (int i = 0; i < countRoleObj; i++)
            {
                Role roleData = roleDataList[i];
                string formatedRoleData = string.Format("|{0,30}|{1,20}|{2,20}|{3,30}|", roleData.Name, roleData.Location, roleData.Department, roleData.Description);
                Console.WriteLine(formatedRoleData);
                Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            }
        }
    }
}
