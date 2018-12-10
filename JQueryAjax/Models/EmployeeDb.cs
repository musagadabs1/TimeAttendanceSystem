using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JQueryAjax.Models
{
    public class EmployeeDb
    {
        readonly string conString = ConfigurationManager.ConnectionStrings["employeeCon"].ConnectionString;
        public List<Employee> GetEmployees()
        {
            
            try
            {
                List<Employee> employees = new List<Employee>();
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("SelectEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Name = reader["Name"].ToString(),
                            Position = reader["Position"].ToString(),
                            Office = reader["Office"].ToString(),
                            Salary = Convert.ToSingle(reader["Salary"])
                        });
                    }
                }
                return employees;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddEmployee(Employee emp)
        {
            try
            {
                var result = 0;
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("InsertUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", emp.Name);
                    cmd.Parameters.AddWithValue("@position", emp.Position);
                    cmd.Parameters.AddWithValue("@office", emp.Office);
                    cmd.Parameters.AddWithValue("@salary", emp.Salary);
                    cmd.Parameters.AddWithValue("@action", "Insert");
                    result = cmd.ExecuteNonQuery();
                }
                return result;
            }
            catch (Exception ex) 
            {

                throw ex;
            }
        }

        public int UpdateEmployee(Employee emp)
        {
            try
            {
                var result = 0;
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("InsertUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", emp.Name);
                    cmd.Parameters.AddWithValue("@id", emp.Id);
                    cmd.Parameters.AddWithValue("@position", emp.Position);
                    cmd.Parameters.AddWithValue("@office", emp.Office);
                    cmd.Parameters.AddWithValue("@salary", emp.Salary);
                    cmd.Parameters.AddWithValue("@action", "Update");
                    result = cmd.ExecuteNonQuery();
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteEmployee(int id)
        {
            int result = 0;
            try
            {
                //var result = 0;
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("DeleteEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@name", emp.Name);
                    cmd.Parameters.AddWithValue("@id", id);
                    //cmd.Parameters.AddWithValue("@position", emp.Position);
                    //cmd.Parameters.AddWithValue("@office", emp.Office);
                    //cmd.Parameters.AddWithValue("@salary", emp.Salary);
                    //cmd.Parameters.AddWithValue("@action", "Update");
                    result = cmd.ExecuteNonQuery();
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}