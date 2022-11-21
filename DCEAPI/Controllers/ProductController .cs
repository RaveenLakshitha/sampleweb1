using DCE_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DCEAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ProductController> logger;
        public ProductController(IConfiguration config) {
            this.configuration = config;
        }
        [NonAction]
        public List<Product> LoadList()
        {
            List<Product> ProductList = new List<Product>();
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand("", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTableProduct = new DataTable();

            for (int i = 0; i < dataTableProduct.Rows.Count; i++) {
                Product prod = new Product();
                prod.ProductId = new Guid(dataTableProduct.Rows[i]["ProductId"].ToString());
                prod.ProductName = dataTableProduct.Rows[i]["ProductName"].ToString();
                prod.UnitPrice = Convert.ToDecimal(dataTableProduct.Rows[i]["UnitPrice"]);
                prod.SupplierId = new Guid(dataTableProduct.Rows[i]["SupplierId"].ToString());
                prod.CreatedOn = Convert.ToDateTime(dataTableProduct.Rows[i]["CreatedOn"]);
                prod.IsActive = Convert.ToBoolean(dataTableProduct.Rows[i]["IsActive"]);
                ProductList.Add(prod);
            }
            return ProductList;
        }
       

        [HttpGet]
        public List<Product> GetProducts() {
                return LoadList();
            
        }

        [HttpPost]
        public string AddProduct(Product Obj) {
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(
                "Insert into Product values('" + Obj.ProductId + "','" + Obj.ProductName + "','" + Obj.UnitPrice + "','" + Obj.SupplierId + "','" + Obj.CreatedOn + "'," + Obj.IsActive + "')", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "Product Added Successfully!";
            
        }

        [HttpPut]
        public string UpdateProduct(string Id)
        {
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(
                "Update from Product where ProductId = '" + Id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "Product Updated Successfully!";
        }

        [HttpDelete]
        public string DeleteProduct(string Id)
        {
            var conn = configuration.GetConnectionString("ConnectionStrings");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(
                "Delete from Product where ProductId = '" + Id+"'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "Product Deleted Successfully!";
        }
    }
    }

