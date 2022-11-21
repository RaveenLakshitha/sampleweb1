using DCE_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DCEAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<SupplierController> logger;
        public SupplierController(IConfiguration config) {
            this.configuration = config;
        }
        [NonAction]
        public List<Supplier> LoadList()
        {
            List<Supplier> SupplierList = new List<Supplier>();
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand("", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTableSupplier = new DataTable();

            for (int i = 0; i < dataTableSupplier.Rows.Count; i++) {
                Supplier sup = new Supplier();
                sup.SupplierId = new Guid(dataTableSupplier.Rows[i]["SupplierId"].ToString()); 
                sup.SupplierName = dataTableSupplier.Rows[i]["SupplierName"].ToString();
                sup.CreatedOn = Convert.ToDateTime(dataTableSupplier.Rows[i]["CreatedOn"].ToString());
                sup.IsActive = Convert.ToBoolean(dataTableSupplier.Rows[i]["IsActive"]);
                SupplierList.Add(sup);
            }
            return SupplierList;
        }
       

        [HttpGet]
        public List<Supplier> GetSuppliers() {

            return LoadList();
        }

        [HttpPost]
        public string AddSupplier(Supplier Obj) {
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(
                "Insert into Supplier values('" + Obj.SupplierId + "','" + Obj.SupplierName + "','" + Obj.CreatedOn + "','" + Obj.IsActive + "')", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "Supplier Added Successfully!";
            
        }

        [HttpPut]

        public string UpdateSupplier(string Id)
        {
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(
                "Update from Supplier where itemid = '" + Id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "Supplier Updated Successfully!";
        }

        [HttpDelete]
        public string DeleteSupplier(string Id)
        {
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(
                "Delete from Supplier where itemid = '"+Id+"'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "Supplier Deleted Successfully!";
        }
    }
    }

