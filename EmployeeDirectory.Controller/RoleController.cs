﻿using EmployeeDirectory.Common;
using EmployeeDirectory.Controller.Contract;
using EmployeeDirectory.Models;
using EmployeeDirectory.Services.Contract;

namespace EmployeeDirectory.Controller
{
    public class RoleController : IRoleController
    {
        IRoleService _roleService;
        IDepartmentService _departmentService;
        ILocationService _locationService;
        IValidationService _validationService;
        public RoleController(IRoleService roleService, IValidationService validationService, ILocationService locationService,IDepartmentService departmentService)
        {
            this._roleService = roleService;
            this._validationService = validationService;
            this._departmentService = departmentService;
            this._locationService = locationService;
        }
        public ValidationResult ValidateRoleName(string roleName)
        {
            ValidationResult roleNameValidator = _validationService.ValidateRoleName(roleName);
            return roleNameValidator;
        }
        public ValidationResult ValidateLocation(string location)
        {
            ValidationResult locationValidation = _validationService.ValidateLocation(location);
            return locationValidation;
        }
        public ValidationResult ValidateDepartment(string department)
        {
            ValidationResult departmentValidation = _validationService.ValidateDepartment(department);
            return departmentValidation;
        }
        public void Add(Role role)
        {
            _roleService.AddRole(role);
        }
        public int GetRoleCount()
        {
            return _roleService.GetRoleCount();
        }
        public Role? GetDataById(string roleId)
        {
            return _roleService.GetRoleById(roleId);
        }
        public string? GetRoleId(string roleName, string location, string department)
        {
            return _roleService.GetRoleId(roleName, location, department);
        }
        public List<Role> GetRoles()
        {
            return _roleService.GetRoles();
        }
        public List<string> GetDepartments()
        {
            return _departmentService.GetDepartment();
        }
        public List<string> GetLocations()
        {
            return _locationService.GetLocation();
        }
       
    }
}