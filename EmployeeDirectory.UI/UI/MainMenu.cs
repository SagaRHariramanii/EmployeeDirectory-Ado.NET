using EmployeeDirectory.UI.Contract;

namespace EmployeeDirectory.UI
{
    public class MainMenuOptions:IMainMenu
    {
        IEmployeeManagementMenu _employeeManagementMenu;
        IRoleManagmentMenu _roleManagmentMenu;
        public MainMenuOptions(IEmployeeManagementMenu employeeManagementMenu,IRoleManagmentMenu roleManagmentMenu)
        {
            this._employeeManagementMenu = employeeManagementMenu;
            this._roleManagmentMenu = roleManagmentMenu;
        }
        public void DisplayMainMenuOptions()
        {
            _displayMainMenuOptions:
            Console.WriteLine("1. Employee Management");
            Console.WriteLine("2. Role Management");
            Console.WriteLine("3. Exit");
            Console.Write("Choice = ");
            int mainMenuOptionChoice= Parser.ParseToInt(Console.ReadLine()!);
            if (mainMenuOptionChoice==-1)
            {
                Console.WriteLine("Invalid Choice Select Again");
                DisplayMainMenuOptions();
            }
            else
            {
                switch (mainMenuOptionChoice)
                {
                    case 1:
                        _employeeManagementMenu.EmployeeManagmentMenuOptions();
                        goto _displayMainMenuOptions;

                    
                    case 2:
                        _roleManagmentMenu.RoleManagmentMenuOptions();
                        goto _displayMainMenuOptions;

                       
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;

                }
            }
        }
    }
}
