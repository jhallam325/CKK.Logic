using CKK.DB.Interfaces;
using CKK.Logic.Models;
using System.Data;
using Dapper;

namespace CKK.DB.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public OrderRepository(IConnectionFactory connection)
        {
            connectionFactory = connection;
        }

        public IConnectionFactory Connection { get; }

        public int Add(Order entity)
        {
            // We need to insert data into the Orders database table

            // We need to make a connection with the database:
            IDbConnection connection = connectionFactory.GetConnection;

            // This is my SQL Query
            string SQLQuery = "INSERT INTO Orders (OrderId, OrderNumber, CustomerId, ShoppingCartId)" +
                " VALUES (@OrderId, @OrderNumber, @CustomerId, @ShoppingCartId)";

            using (connection)
            {
                connection.Open();

                // This actually queries the database with the above query
                int result = connection.Execute(SQLQuery, entity);
                return result;
            }
        }

        public int Delete(int orderId)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "DELETE FROM Orders WHERE OrderId = @OrderId";

            using (connection)
            {
                connection.Open();
                return connection.Execute(SQLQuery, new {OrderId = orderId });
            }
        }

        public Order Get(int orderId)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "SELECT * FROM Orders WHERE OrderId = @OrderId";

            using (connection)
            {
                connection.Open();
                return connection.QuerySingleOrDefault<Order>(SQLQuery, new {OrderId = orderId});
            }
        }

        public Order GetById(int id)
        {
            string sql = $"SELECT * FROM Orders WHERE OrderId = {id}";
            using (var connection = connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Order>(sql);
                return result;
            }
        }

        public List<Order> GetAll()
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "SELECT * FROM Orders";
            using (connection)
            {
                connection.Open();
                List<Order> ordersList = connection.Query<Order>(SQLQuery).ToList();
                return ordersList;
            }
        }
        
        public Order GetOrderByCustomerId(int customerId)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "SELECT * FROM Orders WHERE CustomerId = @CustomerId";

            using (connection)
            {
                connection.Open();
                return connection.QuerySingleOrDefault<Order>(SQLQuery, new {CustomerId = customerId});
            }
        }

        public int Update(Order entity)
        {
            IDbConnection connection = connectionFactory.GetConnection;

            string SQLQuery =
                    "UPDATE Orders " +
                    "SET (OrderNumber = @OrderNumber, " +
                        "CustomerId = @CustomerId, " +
                        "ShoppingCartId = @ShoppingCartId)" +
                    "WHERE OrderId = @OrderId";

            using (connection)
            {
                connection.Open();
                return connection.Execute(SQLQuery, entity);
            }
        }
    }
}
