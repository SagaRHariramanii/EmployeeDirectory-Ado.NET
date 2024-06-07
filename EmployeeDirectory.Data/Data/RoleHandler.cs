using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeDirectory.Data.Data
{
    public class RoleHandler : IRoleHandler
    {
        IDbConnection Connection;
        public RoleHandler(IDbConnection connection)
        {
            this.Connection = connection;
        }
        public void AddData(Role role)
        {
            using(SqlConnection conn = Connection.GetConnection())
            {
                string query = $"Insert Into Role (Id,Name,Description,Location,Department) values(@Id,@Name,@Description,(select ID from Location where Name=@Location),(select ID from Department where Name=@Department)) ";
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", role.ID);
                    cmd.Parameters.AddWithValue("@Name", role.Name);
                    cmd.Parameters.AddWithValue("@Description", role.Description);
                    cmd.Parameters.AddWithValue("@Location", role.Location);
                    cmd.Parameters.AddWithValue("@Department", role.Department);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public List<Role> GetData()
        {
            List<Role> Role_List = new List<Role>();
            using (SqlConnection conn = Connection.GetConnection())
            {
                string query = "Select Role.ID,Role.Name As Name,Location.Name As Location,Department.Name As Department,Description from Role Inner Join Department on Role.Department=Department.ID Inner join Location on Role.Location=Location.ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Role role = new Role
                        {
                            ID = reader["ID"].ToString()!,
                            Name = reader["Name"].ToString()!,
                            Description = reader["Description"].ToString()!,
                            Location = (reader["Location"].ToString()!),
                            Department = (reader["Department"].ToString()!)
                        };
                        Role_List.Add(role);
                    }
                    conn.Close();
                }
            }
            return Role_List;
        }
        public void Update(string roleId, string fieldName, string fieldInputData)
        {
            using (SqlConnection conn = Connection.GetConnection())
            {
                string query = $"Update Role SET @fieldName=@fieldInputData Where Id= @roleId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fieldName", fieldName);
                    cmd.Parameters.AddWithValue("@fieldInputData", fieldInputData);
                    cmd.Parameters.AddWithValue("@roleId", roleId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public void Delete(string roleId)
        {
            using (SqlConnection conn = Connection.GetConnection())
            {
                string query = $"Update Role SET IsDeleted='1' Where Id=@roleId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@roleId",roleId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
    
}
