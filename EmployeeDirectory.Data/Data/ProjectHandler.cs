using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeDirectory.Data.Data
{
    public class ProjectHandler : IProjectHandler
    {
        IDbConnection Connection;

        public ProjectHandler(IDbConnection connection)
        {
            this.Connection = connection;
        }

        public List<Project> GetData()
        {
            List<Project> ProjectList = new List<Project>();
            using (SqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                string query = "Select Id,Name from Project";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Project project = new Project()
                        {
                            ID = ((int)reader["Id"]),
                            Name = reader["Name"].ToString()!
                        };
                        ProjectList.Add(project);
                    }
                }
                conn.Close();
            }
            return ProjectList;
        }
    }
}
