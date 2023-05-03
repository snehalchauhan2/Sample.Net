using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCrudDatabase3mar.Model
{
    public class DAL
    {
        public Response GetAllEmployees(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Employee", connection);
            DataTable dt = new DataTable();
            List<Employee> listEmployees = new List<Employee>();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee employee = new Employee();
                    employee.EmployeeID = Convert.ToInt32(dt.Rows[i]["EmployeeID"]);
                    employee.Firstname = dt.Rows[i]["Firstname"].ToString();
                    employee.Lastname = dt.Rows[i]["Lastname"].ToString();
                    listEmployees.Add(employee);


                }
            }
            if (listEmployees.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data found";
                response.listEmployee = listEmployees;

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Data Not found";
                response.listEmployee = listEmployees;

            }

            return response;
        }
        public Response GetEmployeeById(SqlConnection connection,int id)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Employee where EmployeeID='"+ id + "'", connection);
            DataTable dt = new DataTable();
            Employee Employees = new Employee();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Employee employee = new Employee();
                employee.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"]);
                employee.Firstname = dt.Rows[0]["Firstname"].ToString();
                employee.Lastname = dt.Rows[0]["Lastname"].ToString();

                response.StatusCode = 200;
                response.StatusMessage = "Data found";
                response.Employee = employee;
             
            }
           
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Data Not found";
                response.Employee = null;

            }

            return response;




        }
        public Response AddEmployee(SqlConnection connection, Employee employee)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Employee(Firstname,Lastname)VALUES('" + employee.Firstname + "','" + employee.Lastname + "')", connection);
            //SqlCommand cmd = new SqlCommand("Insert into Employee(EmployeeID,Firstname,Lastname)VALUES('"+employee.EmployeeID+ "','" + employee.Firstname+ "','" + employee.Lastname+ "')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee Added";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No data inserted";
            }

            return response;
         }
        public Response UpdateEmployee(SqlConnection connection,Employee employee)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Update Employee SET Firstname ='" + employee.Firstname + "',Lastname='" + employee.Lastname + "' WHERE EmployeeID ='" + employee.EmployeeID + "'", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee Data Updated";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No data Updated";
            }

            return response;
        }

        public Response DeleteEmployee(SqlConnection connection,int id)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("delete from Employee where EmployeeID ='" + id + "' ", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee Data Deleted";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No data Deleted";
            }

            return response;
        }
    }
}



