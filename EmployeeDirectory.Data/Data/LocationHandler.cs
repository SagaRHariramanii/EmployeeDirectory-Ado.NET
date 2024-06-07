using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeDirectory.Data.Data
{
    public class LocationHandler : ILocationHandler
    {
        IDbConnection Connection;
        public LocationHandler(IDbConnection connection)
        {
            Connection = connection;
        }
        public List<Location> GetData()
        {
            List<Location> locationList = new List<Location>();
            using (SqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                string query = "Select ID,Name from Location";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Location location = new Location()
                        {
                            ID = ((int)reader["ID"]),
                            Name = reader["Name"].ToString()!
                        };
                        locationList.Add(location);
                    }
                }
                conn.Close();
            }
            return locationList;
        }
    }
}
