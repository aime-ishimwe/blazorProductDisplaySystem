
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ProductDisplaySystem.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class GridController : ControllerBase
    {
        private readonly string ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=Products;Integrated Security=True";

        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
        }

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int CategoryId { get; set; }
            public Category Category { get; set; } = new();
        }

        public class ProductCreateDto
        {
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int CategoryId { get; set; }
        }

        [HttpGet("products")]
        public List<Product> GetProductsData()
        {
            var products = new List<Product>();

            string query = @"
                SELECT 
                    p.Id AS ProductId, p.Name AS ProductName, p.Price, p.CategoryId,
                    c.Id AS CategoryId, c.Name AS CategoryName, c.Description
                FROM dbo.Products p
                INNER JOIN dbo.Categories c ON p.CategoryId = c.Id
                ORDER BY p.Id";

            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var product = new Product
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ProductId")),
                    Name = reader.GetString(reader.GetOrdinal("ProductName")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                    Category = new Category
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                        Name = reader.GetString(reader.GetOrdinal("CategoryName")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("Description"))
                    }
                };
                products.Add(product);
            }

            return products;
        }

        [HttpGet("categories")]
        public List<Category> GetCategories()
        {
            var categories = new List<Category>();
            string query = "SELECT Id, Name, Description FROM dbo.Categories ORDER BY Name";

            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("Description"))
                });
            }

            return categories;
        }

        [HttpPost("products")]
        public IActionResult AddProduct([FromBody] ProductCreateDto newProductDto)
        {
            if (newProductDto == null ||
                string.IsNullOrWhiteSpace(newProductDto.Name) ||
                newProductDto.Price <= 0 ||
                newProductDto.CategoryId <= 0)
            {
                return BadRequest("Invalid product data.");
            }

            int newId;
            using var conn = new SqlConnection(ConnectionString);
            string insertQuery = @"
                INSERT INTO dbo.Products (Name, Price, CategoryId)
                VALUES (@Name, @Price, @CategoryId);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            conn.Open();
            using var cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@Name", newProductDto.Name);
            cmd.Parameters.AddWithValue("@Price", newProductDto.Price);
            cmd.Parameters.AddWithValue("@CategoryId", newProductDto.CategoryId);

            newId = (int)cmd.ExecuteScalar()!;

            return CreatedAtAction(nameof(GetProductsData), new { id = newId }, new { Id = newId });
        }

        [HttpDelete("products/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            using var conn = new SqlConnection(ConnectionString);
            string deleteQuery = "DELETE FROM dbo.Products WHERE Id = @Id";

            conn.Open();
            using var cmd = new SqlCommand(deleteQuery, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected == 0)
                return NotFound($"Product with Id {id} not found.");

            return NoContent();
        }
    }
}