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
            string SQLQuery = "INSERT INTO ShoppingCartItems (ShoppingCartId, ProductId, Quantity) " +
                "VALUES (@ShoppingCartId, @ProductId, @Quantity)";

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
                        //var test = UpdateAsync(shopitem);
                        var test = Update(shopitem);
                    }
                    else
                    {
                        //New product for the cart so add it
                        //var test = AddAsync(shopitem);
                        var test = Add(shopitem);
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

        public decimal GetTotal(int shoppingCartId)
        {
            using (var conn = connectionFactory.GetConnection)
            {
                var items = SqlMapper.Query<ShoppingCartItem>(conn, @"SELECT * FROM ShoppingCartItems WHERE dbo.ShoppingCartItems.ShoppingCartId = @ShoppingCartId", new { ShoppingCartId = shoppingCartId }).ToList();
                List<decimal> total = new List<decimal>();
                ProductRepository _productRepository = new ProductRepository(connectionFactory);

                foreach (var item in items)
                {
                    var product = _productRepository.Get(item.ProductId);
                    total.Add(product.Price * (decimal)item.Quantity);
                }
                return total.Sum();
            }
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
