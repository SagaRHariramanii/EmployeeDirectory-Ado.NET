using EmployeeDirectory.UI;
using Microsoft.Extensions.Configuration;
using EmployeeDirectory.Data.Data;
using Microsoft.Extensions.DependencyInjection;
using EmployeeDirectory.Services.Contract;
using EmployeeDirectory.Services;
using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Data;
using EmployeeDirectory.Services.Services;
using EmployeeDirectory.Controller.Contract;
using EmployeeDirectory.Controller;
using EmployeeDirectory.UI.Contract;
using EmployeeDirectory.UI.Menus;
using EmployeeDirectory.Common.Services;

namespace MainMenu
{
    
    public class EmployeeDirectory
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connectionString = configBuilder.GetSection("ConnectionStrings")["MyDBConnectionString"]!;
            if (connectionString != null)
            {
                services.AddScoped<IDbConnection>(db => new DbConnection(connectionString));
            }
            else
            {
                throw new Exception("Error");
            }
            
            services.AddScoped<IEmployeeHandler, EmployeeHandler>();
            services.AddScoped<IRoleHandler, RoleHandler>();
            services.AddScoped<IManagerHandler, ManagerHandler>();
            services.AddScoped<IProjectHandler, ProjectHandler>();
            services.AddScoped<IDepartmentHandler, DepartmentHandler>();
            services.AddScoped<ILocationHandler, LocationHandler>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IManagerService,ManagerService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IDepartmentService,DepartmentService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IEmployeeController, EmployeeController>();
            services.AddScoped<IRoleController, RoleController>();
            services.AddScoped<IEmployeeManagementMenu,EmployeeManagementMenu>();
            services.AddScoped<IRoleManagmentMenu, RoleManagmentMenu>();
            services.AddScoped<IMainMenu,MainMenuOptions>();

            // Build the service provider and store it in a variable
            
            var serviceProvider = services.BuildServiceProvider();
            var mainMenu = serviceProvider.GetRequiredService<IMainMenu>();

            mainMenu.DisplayMainMenuOptions();
        }
    }
}