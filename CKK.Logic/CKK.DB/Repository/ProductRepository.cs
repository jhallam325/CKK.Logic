
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/

using CKK.DB.Interfaces;
using CKK.DB.UOW;
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
            string SQLQuery = "INSERT INTO Products (Id, Price, Quantity, Name) " +
                "VALUES (@Id, @Price, @Quantity, @Name)";
            using (IDbConnection connection = connectionFactory.GetConnection)
            {
                connection.Open();
                int result = connection.Execute(SQLQuery, entity);
                return result;
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
                Product result = connection.QuerySingleOrDefault<Product>(SQLQuery, new { Id = id });
                return result;
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
                "SET (Price = @Price, Quantity = @Quantity, Name = @Name) " +
                "WHERE (Id = @Id)";

            using (connection)
            {
                connection.Open();
                return connection.Execute(SQLQuery, entity);
            }
        }

        internal object GetByIdAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
