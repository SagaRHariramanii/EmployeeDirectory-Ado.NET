using EmployeeDirectory.Data.Contract;
using EmployeeDirectory.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeDirectory.Data.Data
{
    public class EmployeeHandler : IEmployeeHandler
    {
        IDbConnection _connection;

        //explore about sql injection attacks
        //make these handlers robust
        public EmployeeHandler(IDbConnection connection)
        {
            this._connection = connection;
        }
        public void AddEmployee(Employee employee)
        {
            using(SqlConnection conn=_connection.GetConnection())
            {
                string query = $"Insert Into Employee(EmpId,FirstName,LastName,Email,Dob,PhoneNo,JoiningDate,ManagerId,ProjectId,RoleId,IsDeleted) " +
                    $"Values(@EmpId,@FirstName,@LastName,@Email,@Dob,@PhoneNo,@JoiningDate,(Select Manager.Id from (Select Manager.Id ,Employee.FirstName+' '+Employee.LastName As Name from Manager Inner Join Employee on Manager.EmployeeId=Employee.Id) As Manager Where Manager.Name=@ManagerName),(Select Id from Project Where Name=@ProjectName),@RoleId,@IsDeleted)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmpId" , employee.EmpId);
                    cmd.Parameters.AddWithValue("@FirstName",employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName",employee.LastName);
                    cmd.Parameters.AddWithValue("@Email",employee.Email);
                    cmd.Parameters.AddWithValue("@Dob", employee.Dob);
                    cmd.Parameters.AddWithValue("@PhoneNo",employee.PhoneNo);
                    cmd.Parameters.AddWithValue("@JoiningDate",employee.JoiningDate);
                    cmd.Parameters.AddWithValue("@ManagerName", employee.ManagerName);
                    cmd.Parameters.AddWithValue("@ProjectName",employee.ProjectName);
                    cmd.Parameters.AddWithValue("@RoleId",employee.RoleId);
                    cmd.Parameters.AddWithValue("@IsDeleted", employee.IsDeleted);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> Employees_List = new List<Employee>();
            using (SqlConnection conn = _connection.GetConnection())
            {
                conn.Open();
                string query = "Select Employee.EmpId,Employee.FirstName,Employee.LastName,Employee.Email,Employee.Dob,Employee.PhoneNo,Employee.JoiningDate,Employee.RoleId,Employee.IsDeleted,(ManagerEmployee.FirstName+' '+ManagerEmployee.LastName)As ManagerName,Project.Name As ProjectName\r\nfrom Employee Inner Join Manager on Employee.ManagerId=Manager.Id \r\nINNER JOIN Employee AS ManagerEmployee ON Manager.EmployeeId = ManagerEmployee.Id \r\nInner Join Project On Employee.ProjectId=Project.Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee emp = new Employee
                        {
                            EmpId = reader["EmpId"].ToString()!,
                            FirstName = reader["FirstName"].ToString()!,
                            LastName = reader["LastName"].ToString()!,
                            Email = reader["Email"].ToString()!,
                            Dob = ((DateTime)reader["Dob"]),
                            PhoneNo=reader["PhoneNo"].ToString()!,
                            JoiningDate = ((DateTime)reader["JoiningDate"]),
                            ManagerName = reader["ManagerName"].ToString()!,
                            ProjectName = reader["ProjectName"].ToString()!,
                            RoleId = reader["RoleId"].ToString()!,
                            IsDeleted = ((bool)reader["IsDeleted"])

                        };
                        Employees_List.Add(emp);
                        
                    }
                }
                conn.Close();

            }
            return Employees_List;

        }
        public Employee? GetEmployeeById(string empId)
        {
            Employee? employee=null;
            using (SqlConnection conn = _connection.GetConnection())
            {
                conn.Open();
                string query = "Select Employee.EmpId,Employee.FirstName,Employee.LastName,Employee.Email,Employee.Dob,Employee.PhoneNo,Employee.JoiningDate,Employee.RoleId,Employee.IsDeleted,(ManagerEmployee.FirstName+' '+ManagerEmployee.LastName)As ManagerName,Project.Name As ProjectName\r\nfrom Employee Inner Join Manager on Employee.ManagerId=Manager.Id \r\nINNER JOIN Employee AS ManagerEmployee ON Manager.EmployeeId = ManagerEmployee.Id \r\nInner Join Project On Employee.ProjectId=Project.Id  Where Employee.EmpId=@EmpId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee emp = new Employee
                        {
                            EmpId = reader["EmpId"].ToString()!,
                            FirstName = reader["FirstName"].ToString()!,
                            LastName = reader["LastName"].ToString()!,
                            Email = reader["Email"].ToString()!,
                            Dob = ((DateTime)reader["Dob"]),
                            PhoneNo = reader["PhoneNo"].ToString()!,
                            JoiningDate = ((DateTime)reader["JoiningDate"]),
                            ManagerName = reader["ManagerName"].ToString()!,
                            ProjectName = reader["ProjectName"].ToString()!,
                            RoleId = reader["RoleId"].ToString()!,
                            IsDeleted = ((bool)reader["IsDeleted"])

                        };
                        employee = emp;
                    }
                    conn.Close();
                }
            }
            return employee;

        }
        public void Update<T>(string empId, Enum fieldName, T fieldInputData)
        {
            using (SqlConnection conn = _connection.GetConnection())
            {
                string query = $"Update Employee SET {fieldName}=@FieldInputData Where EmpId=@EmpId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FieldInputData", fieldInputData);
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public void Delete(string empId)
        {
            using (SqlConnection conn = _connection.GetConnection())
            {
                string query = $"Update Employee SET IsDeleted='1' Where EmpId=@EmpId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

