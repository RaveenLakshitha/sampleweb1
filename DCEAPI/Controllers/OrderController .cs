using DCE_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DCEAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<OrderController> logger;
        public OrderController(IConfiguration config) {
            this.configuration = config;
        }
        [NonAction]
        public List<Order> LoadList()
        {
            List<Order> OrderList = new List<Order>();
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand("", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTableOrder = new DataTable();

            for (int i = 0; i < dataTableOrder.Rows.Count; i++) {
                Order ord = new Order();
                ord.OrderId = new Guid(dataTableOrder.Rows[i]["OrderId"].ToString()); 
                ord.ProductId = new Guid(dataTableOrder.Rows[i]["ProductId"].ToString());
                ord.OrderType = Convert.ToInt32(dataTableOrder.Rows[i]["OrderType"]);
                ord.OrderBy = new Guid(dataTableOrder.Rows[i]["OrderBy"].ToString());
                ord.OrderedOn = Convert.ToDateTime(dataTableOrder.Rows[i]["OrderedOn"]);
                ord.ShippedOn = Convert.ToDateTime(dataTableOrder.Rows[i]["ShippedOn"]);
                ord.OrderStatus = Convert.ToInt32(dataTableOrder.Rows[i]["OrderStatus"]);
                ord.IsActive = Convert.ToBoolean(dataTableOrder.Rows[i][""]);
                OrderList.Add(ord);
            }
            return OrderList;
        }
       

        [HttpGet]
        public List<Order> GetOrders() {

            return LoadList();
        }

        [HttpPost]
        public string AddOrder(Order Obj) {
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(
                "Insert into Order values('" + Obj.OrderId + "','" + Obj.ProductId + "','" + Obj.OrderType + "','" + Obj.OrderBy + "','" + Obj.OrderedOn + "','" + Obj.ShippedOn + "','" + Obj.OrderStatus + "','" + Obj.IsActive + "')", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "Order Added Successfully!";
            
        }

        [HttpPut]
        public string UpdateOrder(string Id)
        {
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(
                "Update from Order where itemid = '" + Id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "Order Updated Successfully!";
        }

        [HttpDelete]
        public string DeleteOrder(string Id)
        {
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(
                "Delete from Order where itemid = '"+Id+"'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "Order Deleted Successfully!";
        }
    }
    }

