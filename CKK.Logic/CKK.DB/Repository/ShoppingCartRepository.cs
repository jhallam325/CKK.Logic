using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;
using System.Data;

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
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "INSERT INTO ShoppingCartItems (ShoppingCartId, ProductId, Quantity) " +
                "VALUES (@ShoppingCartId, @ProductId, @Quantity)";

            using (connection)
            {
                connection.Open();
                return connection.Execute(SQLQuery, entity);
            }
        }

        public ShoppingCartItem AddToCart(int shoppingCardId, int productId, int quantity)
        {
            IDbConnection connection = connectionFactory.GetConnection;
            using (connection)
            {
                ProductRepository productRepository = new ProductRepository(connectionFactory);
                Product item = productRepository.Get(productId);

                ShoppingCartItem ProductItems = GetProducts(shoppingCardId).Find(x => x.ProductId == productId);

                ShoppingCartItem shopitem = new ShoppingCartItem()
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
                        int test = Update(shopitem);
                    }
                    else
                    {
                        //New product for the cart so add it
                        int test = Add(shopitem);
                    }
                }
                return shopitem;
            }
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
            IDbConnection connection = connectionFactory.GetConnection;

            using (connection)
            {
                List<ShoppingCartItem> items = SqlMapper.Query<ShoppingCartItem>(connection, 
                    @"SELECT * FROM ShoppingCartItems WHERE dbo.ShoppingCartItems.ShoppingCartId = @ShoppingCartId", 
                    new { ShoppingCartId = shoppingCartId }).ToList();
                List<decimal> total = new List<decimal>();
                ProductRepository productRepository = new ProductRepository(connectionFactory);

                foreach (var item in items)
                {
                    Product product = productRepository.Get(item.ProductId);
                    total.Add(product.Price * item.Quantity);
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
            IDbConnection connection = connectionFactory.GetConnection;
            string SQLQuery = "UPDATE ShoppingCartItems " +
                "SET ShoppingCartId = @ShoppingCartId, ProductId = @ProductId, Quantity = @Quantity " +
                "WHERE ShoppingCartId = @ShoppingCartId AND ProductId = @ProductId";
            using (connection)
            {
                connection.Open();
                return connection.Execute(SQLQuery, entity);
            }
        }
    }
}
