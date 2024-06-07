using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeDirectory.Data.Data
{
    public class DepartmentHandler : IDepartmentHandler
    {
        IDbConnection _connection;
        public DepartmentHandler(IDbConnection connection)
        {
            this._connection = connection;
        }
        public List<Department> GetData()
        {
            List<Department> departmentList = new List<Department>();
            using (SqlConnection conn = _connection.GetConnection())
            {
                conn.Open();
                string query = "Select ID,Name from Department";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Department department = new Department()
                        {
                            ID = ((int)reader["ID"]),
                            Name = reader["Name"].ToString()!
                        };
                        departmentList.Add(department);
                    }
                }
                conn.Close();
            }
            return departmentList;
        }
    }
}
