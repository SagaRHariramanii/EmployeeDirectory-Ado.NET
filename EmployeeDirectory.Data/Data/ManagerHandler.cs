using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeDirectory.Data.Data
{
    public class ManagerHandler : IManagerHandler
    {
        IDbConnection Connection;

        public ManagerHandler(IDbConnection connection)
        {
            this.Connection = connection;
        }

        public List<Manager> GetData()
        {
            List<Manager> managerList = new List<Manager>();
            using (SqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                string query = "select Manager.Id,FirstName+' '+LastName as Name from Manager Inner Join Employee on Manager.EmployeeId=Employee.Id;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Manager manager = new Manager()
                        {
                            ID = ((int)reader["Id"]),
                            Name = reader["Name"].ToString()!
                        };
                        managerList.Add(manager);
                    }
                }
                conn.Close();
            }
            return managerList;
        }
    }
}
