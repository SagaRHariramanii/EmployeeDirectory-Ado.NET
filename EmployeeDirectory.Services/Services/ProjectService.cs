using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services.Contract;

namespace EmployeeDirectory.Services.Services
{
    public class ProjectService : IProjectService
    {
        readonly IProjectHandler ProjectHandler;
        public ProjectService(IProjectHandler projectHandler)
        {
            this.ProjectHandler = projectHandler;
        }
        public List<string> GetProjects()
        {
            List<string> Project = [];
            List<Project> ProjectList = ProjectHandler.GetData();
            for (int i = 0; i < ProjectList.Count; i++)
            {
                Project.Add(ProjectList[i].Name);
            }
            return Project;
        }

        public int GetProjectId(string name)
        {
            List<Project> ProjectList = ProjectHandler.GetData();
            Project projectList = (from project in ProjectList where (project.Name).Equals(name) select project).FirstOrDefault()!;
            return projectList.ID;
        }
    }
}
