﻿using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services.Contract;

namespace EmployeeDirectory.Services.Services
{
    public class ProjectService : IProjectService
    {
        readonly IProjectHandler _projectHandler;
        public ProjectService(IProjectHandler projectHandler)
        {
            this._projectHandler = projectHandler;
        }
        public List<Project> GetProjects()
        {
            return _projectHandler.GetData();
        }
        public string? GetProjectName(int id)
        {
            return _projectHandler.GetProjectNameById(id);
        }
    }
}
