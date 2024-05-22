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
using System.Data.SqlClient;

namespace CKK.DB.Repository
{
    internal class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public ShoppingCartRepository(IConnectionFactory connection)
        {
            connectionFactory = connection;
        }

        public IConnectionFactory Connection { get; }

        public int Add(ShoppingCartItem entity)
        {
            string SQLQuery = "INSERT INTO ShoppingCartItems (Id, ShoppingCartId, ProductId, Quantity) " +
                "VALUES (@Id, @ShoppingCartId, @ProductId, @Quantity)";

            using (IDbConnection connection = connectionFactory.GetConnection)
            {
                connection.Open();
                int result = connection.Execute(SQLQuery, entity);
                return result;
            }
        }

        // This AddToCart method does not work because the Async methods aren't implimented
        public ShoppingCartItem AddToCart(int shoppingCardId, int productId, int quantity)
        {
            using (var conn = connectionFactory.GetConnection)
            {
                ProductRepository productRepository = new ProductRepository(connectionFactory);
                //var item = productRepository.GetByIdAsync(productId).Result;
                Product item = productRepository.Get(productId);

                // ShoppingCartItem
                var ProductItems = GetProducts(shoppingCardId).Find(x => x.ProductId == productId);

                var shopitem = new ShoppingCartItem()
                {
                    ShoppingCartId = shoppingCardId,
                    ProductId = productId,
                    Quantity = quantity
                };

                if (item.Quantity >= quantity)
                {
                    if (ProductItems != null)
                    {
                        //Product already in cart so update quantity
                        var test = UpdateAsync(shopitem);
                    }
                    else
                    {
                        //New product for the cart so add it
                        var test = AddAsync(shopitem);
                    }
                }
                return shopitem;
            }
        }
        

        private object AddAsync(ShoppingCartItem shopitem)
        {
            throw new NotImplementedException();
        }

        private object UpdateAsync(ShoppingCartItem shopitem)
        {
            throw new NotImplementedException();
        }

        public int ClearCart(int shoppingCartId)
        {
            // Delete the cart from the database
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "DELETE FROM ShoppingCartItems WHERE ShoppingCartId  = @ShoppingCartId ";

            using (connection)
            {
                connection.Open();
                return connection.Execute(SQLQuery, new { ShoppingCartId = shoppingCartId });
            }
        }

        public List<ShoppingCartItem> GetProducts(int shoppingCartId)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "SELECT * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
            using (connection)
            {
                connection.Open();
                List<ShoppingCartItem> shCartItems = connection.Query<ShoppingCartItem>(SQLQuery, new { ShoppingCartId = shoppingCartId }).ToList();
                return shCartItems;
            }
        }

        public decimal GetTotal(int ShoppingCartId)
        {
            // Test this to see if it works compared to the official solution on discord
            List<ShoppingCartItem> shCartItems = GetProducts(ShoppingCartId);
            int quantity = shCartItems[0].Quantity;

            int requestedProductId = shCartItems[0].ProductId;
            decimal price;


            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "SELECT Price FROM Products WHERE ProductId = @ProductId";
            using (connection)
            {
                connection.Open();
                price = connection.ExecuteScalar<decimal>(SQLQuery, new { ProductId = requestedProductId });
            }

            return quantity * price;
        }

        public void Ordered(int shoppingCartId)
        {
            // Once a cart is ordered, we just delete it
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "DELETE FROM ShoppingCartItems WHERE ShoppingCartId = @shoppingCartId";

            using (connection)
            {
                connection.Open();
                connection.Execute(SQLQuery, new { ShoppingCartId = shoppingCartId });
            }
        }

        public int Update(ShoppingCartItem entity)
        {
            string SQLQuery = "UPDATE ShoppingCartItems " +
                "SET ShoppingCartId = @ShoppingCartId, ProductId = @ProductId, Quantity = @Quantity " +
                "WHERE ShoppingCartId = @ShoppingCartId AND ProductId = @ProductId";
            using (var connection = connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(SQLQuery, entity);
                return result;
            }
        }
    }
}
