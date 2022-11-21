using DCE_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DCEAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<CustomerController> logger;
        public CustomerController(IConfiguration config) {
            this.configuration = config;
        }
        [NonAction]
        public List<Customer> LoadList()
        {
            List<Customer> CustomerList = new List<Customer>();
            var conn = configuration.GetConnectionString("value");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand("Select * from Customer;", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTableCustomer = new DataTable();
            adapter.Fill(dataTableCustomer);
            Console.WriteLine("Rav");
            for (int i = 0; i < dataTableCustomer.Rows.Count; i++) {
                Customer cust = new Customer();
                cust.UserId = new Guid(dataTableCustomer.Rows[i]["UserId"].ToString());
                cust.UserName = dataTableCustomer.Rows[i]["UserName"].ToString();
                cust.FirstName = dataTableCustomer.Rows[i]["Firstname"].ToString();
                cust.LastName = dataTableCustomer.Rows[i]["LastName"].ToString();
                cust.Email = dataTableCustomer.Rows[i]["Email"].ToString();
                cust.CreatedOn = Convert.ToDateTime(dataTableCustomer.Rows[i]["CreatedOn"].ToString()); 
                cust.IsActive = Convert.ToInt32(dataTableCustomer.Rows[i]["IsActive"]);
                
                CustomerList.Add(cust);
            }
            return CustomerList;
        }
       

        [HttpGet]
        public List<Customer> GetCustomers() {
              return LoadList();
        }

        [HttpPost]
        public string AddCustomer(Customer Obj) {
            try {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                    "Insert into Customer values('" + Obj.UserId + "','" + Obj.UserName + "','" + Obj.Email + "','" + Obj.FirstName + "','" + Obj.LastName + "','" + Obj.CreatedOn + "','" + Obj.IsActive + "')", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Customer Added Successfully!";
            } catch (SqlException e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return "NOt";
                    }
        }

        [HttpPut]
        public string UpdateCustomer(string Id)
        {
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(
                "Update from Customer where UserId = '" + Id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "Customer Updated Successfully!";
        }

        [HttpDelete]
        public string DeleteCustomer(string Id)
        {
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(
                "Delete from Customer where UserId = '" + Id+"'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "Customer Deleted Successfully!";
        }
    }
    }

