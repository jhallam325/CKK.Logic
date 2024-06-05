using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;
using System.Data;

namespace CKK.DB.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public ProductRepository(IConnectionFactory connection)
        {
            connectionFactory = connection;
        }

        public IConnectionFactory Connection { get; }

        public int Add(Product entity)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "INSERT INTO Products (Id, Price, Quantity, Name, Picture) " +
                "VALUES (@Id, @Price, @Quantity, @Name, @Picture)";

            using (connection)
            {
                connection.Open();
                return connection.Execute(SQLQuery, entity);
            }
        }

        public int Delete(int id)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "DELETE FROM Products WHERE Id = @Id";

            using (connection)
            {
                connection.Open();
                return connection.Execute(SQLQuery, new { Id = id });
            }
        }

        public Product Get(int id)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "SELECT * FROM Products WHERE Id = @Id";

            using (connection)
            {
                connection.Open();
                return connection.QuerySingleOrDefault<Product>(SQLQuery, new { Id = id });
            }
        }

        public List<Product> GetAll()
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "SELECT * FROM Products";

            using (connection)
            {
                connection.Open();
                List<Product> products = connection.Query<Product>(SQLQuery).ToList();
                return products;
            }
        }

        public List<Product> GetByName(string name)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "SELECT * FROM Products WHERE Name = @Name";

            using (connection)
            {
                connection.Open();
                List<Product> products = connection.Query<Product>(SQLQuery, new {Name = name}).ToList();
                return products;
            }
        }

        public int Update(Product entity)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery =
                "UPDATE Products " +
                "SET Price = @Price, Quantity = @Quantity, Name = @Name, Picture = @Picture " +
                "WHERE Id = @Id";

            using (connection)
            {
                connection.Open();
                return connection.Execute(SQLQuery, entity);
            }
        }

        public List<Product> SortByAsc(string sortValue)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery;

            if (sortValue == "ID Number")
            {
                SQLQuery = "SELECT * FROM Products " +
                "ORDER BY Id";
                
            }
            else if (sortValue == "Quantity")
            {
                SQLQuery = "SELECT * FROM Products " +
                "ORDER BY Quantity";
            }
            else
            {
                SQLQuery = "SELECT * FROM Products " +
                "ORDER BY Price";
            }
            
            using (connection)
            {
                connection.Open();
                List<Product> products = connection.Query<Product>(SQLQuery).ToList();
                return products;
            }
        }

        public List<Product> SortByDesc(string sortValue)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery;

            if (sortValue == "ID Number")
            {
                SQLQuery = "SELECT * FROM Products " +
                "ORDER BY Id DESC";

            }
            else if (sortValue == "Quantity")
            {
                SQLQuery = "SELECT * FROM Products " +
                "ORDER BY Quantity DESC";
            }
            else
            {
                SQLQuery = "SELECT * FROM Products " +
                "ORDER BY Price DESC";
            }

            using (connection)
            {
                connection.Open();
                List<Product> products = connection.Query<Product>(SQLQuery).ToList();
                return products;
            }
        }

        public List<Product> SearchFor(string searchTerm)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string upper = searchTerm.ToUpper();
            string SQLQuery = $"Select * From Products " +
                $"WHERE Name LIKE '%{upper}%'";

            using (connection)
            {
                connection.Open();
                List<Product> products = connection.Query<Product>(SQLQuery).ToList();
                return products;
            }
        }

        // Added for the Create New Item UI page. Need to test
        public int GetId(Product entity)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "SELECT Id FROM Products " +
                "WHERE Name = @Name, Quantity = @Quantity, Price = @Price, Picture = @Picture";

            using (connection)
            {
                connection.Open();
                List<Product> products = connection.Query<Product>(SQLQuery, entity).ToList();
                return products[0].Id;
            }
        }

    }
}
