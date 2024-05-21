
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/

using CKK.DB.Interfaces;
using CKK.Logic.Models;
using System.Data;
using System;
using Dapper;
using System.Net.Mail;
using Microsoft.Data.SqlClient;
using CKK.DB.UOW;
using System.Collections.Generic;
using System.Linq;
using CKK.Logic.Interfaces;

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

            // This is my SQL Query
            string SQLQuery = "INSERT INTO Orders (OrderId, OrderNumber, CustomerId, ShoppingCartId)" +
                " VALUES (@OrderId, @OrderNumber, @CustomerId, @ShoppingCartId)";
            /*
            string SQLQuery = $"INSERT INTO Order (OrderNumber, CustomerId, ShoppingCartId)" +
                $"VALUES ('{entity.OrderNumber}', {entity.CustomerId}, {entity.ShoppingCartId})";
            */

            // We need to make a connection with the database:
            IDbConnection connection = connectionFactory.GetConnection;
            
            using (connection)
            {
                connection.Open();

                // This actually queries the database with the above query
                int result = connection.Execute(SQLQuery, entity);
                return result;

                /*

                // This method returns an int, so maybe the primary key, OrderId, of the order table?
                // If so, hoy would I get it? - Query the database
                string orderIdSearch = $"SELECT OrderId FROM Order WHERE OrderNumber = '{entity.OrderNumber}'";

                // Maybe connection.Query() returns an Order, not an int, even though I select an OrderId.
                // Get the order from the query (an IEneumerable) and cast it to a List<Order> with .ToList()
                List<Order> addedOrder = connection.Query<Order>(orderIdSearch).ToList();

                // Now I can get the OrderId from that first (and only) order in the list
                return addedOrder[0].OrderId;
                */
            }
        }

        public int Delete(int orderId)
        {
            // We need to delete data from the Orders Table
            IDbConnection connection = connectionFactory.GetConnection;

            using (connection)
            {
                connection.Open();

                // This is my SQL Query
                string SQLQuery = "DELETE FROM Orders WHERE OrderId = @OrderId";

                // This actually queries the database with the above query
                return connection.Execute(SQLQuery, new {OrderId = orderId });
            }
        }

            // DELETE FROM table_name WHERE condition;
        

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
