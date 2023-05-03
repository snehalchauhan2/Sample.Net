using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiCrudDatabase3mar.Model;
using WebApiCrudDatabase3mar.CommonMethods;
using Akavache;

namespace WebApiCrudDatabase3mar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpPost]
        [Route("Registration")]

        public string regstration(Registration registration)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Employeeconn").ToString());
            SqlCommand cmd = new SqlCommand("Insert into Registration(Username,Password,Email,IsActive)VALUES('" + registration.Username + "','" + CommonMethods.CommonMethods.convertToEncrypt(registration.Password) + "','" + registration.Email + "','" + registration.IsActive + "')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();

            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read()) regstration = this.Mapping(reader);

            connection.Close();
            if (i>0)
            {
                return "Data inserted";
            }
            else
            {
                return "Error";
            }
           
            
        }

        [HttpPost]
        [Route("Login")]
        public string Login(Registration registration)
        {

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Employeeconn").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * from Registration where Email='"+registration.Email + "' AND Password='" +CommonMethods.CommonMethods.convertToEncrypt(registration.Password)+ "' AND  IsActive='" + registration.IsActive + "'", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return "Valid User";
            }
            else
            {

                return "Invalid User";
            }

        }
        [HttpPost]
        [Route("Loginm")]
        public string Loginm(Registration registration)
        {

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Employeeconn").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * from Registration where Email='" + registration.Email + "' AND Password='" + CommonMethods.CommonMethods.convertToDecrypt(registration.Password) + "' AND  IsActive='" + registration.IsActive + "'", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return "Valid User";
            }
            else
            {

                return "Invalid User";
            }

        }

    }
}
