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
        public void AddRole(Role role)
        {
            using(SqlConnection conn = Connection.GetConnection())
            {
                string query = $"Insert Into Role (Name,Description,Location,Department) values(@Name,@Description,(select ID from Location where Name=@Location),(select ID from Department where Name=@Department)) ";
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
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
        public List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
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
                            ID = (int)reader["ID"],
                            Name = reader["Name"].ToString()!,
                            Description = reader["Description"].ToString()!,
                            Location = (reader["Location"].ToString()!),
                            Department = (reader["Department"].ToString()!)
                        };
                        roles.Add(role);
                    }
                    conn.Close();
                }
            }
            return roles;
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
        public int GetRoleCount()
        {
            int count=0;
            using (SqlConnection conn =Connection.GetConnection())
            {
                string query = "Select count(*) as RoleCount from Role";
                using (SqlCommand cmd = new SqlCommand(query,conn))
                {
                    conn.Open ();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int roleCount = (int)reader["RoleCount"];
                        count = roleCount;
                    }
                    conn.Close();
                    
                }
            }
            return count;
        }
        public Role? GetRoleById(int roleId)
        {
            Role? roleDetail = null;
            using (SqlConnection conn = Connection.GetConnection())
            {
                string query = "Select Role.ID,Role.Name As Name,Location.Name As Location,Department.Name As Department,Description from Role Inner Join Department on Role.Department=Department.ID Inner join Location on Role.Location=Location.ID Where Role.ID=@RoleId";
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@RoleId", roleId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Role role = new Role
                        {
                            ID = (int)reader["ID"],
                            Name = reader["Name"].ToString()!,
                            Description = reader["Description"].ToString()!,
                            Location = (reader["Location"].ToString()!),
                            Department = (reader["Department"].ToString()!)
                        };
                        roleDetail=role;
                    }
                    conn.Close();
                }
                return roleDetail;
            }
        }
        public int? GetRoleId(string roleName, string location, string department)
        {
            int? roleId = null;
            using (SqlConnection conn = Connection.GetConnection())
            {
                string query = "Select ID from Role Where Name=@RoleName and Location=(Select ID from Location where Name=@LocationName) and Department=(Select ID from Department where Name=@DepartmentName) ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@RoleName", roleName);
                    cmd.Parameters.AddWithValue("@LocationName", location);
                    cmd.Parameters.AddWithValue("@DepartmentName", department);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        roleId = (int)reader["ID"];
                    }
                    conn.Close();
                }
                return roleId;
            }

        }
        public void Delete(int roleId)
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
