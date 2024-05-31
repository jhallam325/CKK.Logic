using CKK.Logic.Models;

namespace CKK.tests
{
    public class ShoppingCartTests
    {
        [Fact]
        public void AddRemoveProduct_ShouldAddAndRemoveProductCorrectly()
        {
            Customer customer = new Customer();
            ShoppingCart shoppingCart = new ShoppingCart(customer);
            Product product1 = new Product();
            Product product2 = new Product();
            int quantity1 = 1;
            int quantity2 = 15;
            int removeQuantity1 = 5;
            int removeQuantity2 = 10;

            product1.SetId(1);
            product2.SetId(2);

            shoppingCart.AddProduct(product1);
            shoppingCart.AddProduct(product2, quantity2);

            // Test the constructor with no quantity parameter
            Assert.Equal(quantity1, shoppingCart.GetProduct(1).GetQuantity());

            // Test the constructor with both parameters
            Assert.Equal(quantity2, shoppingCart.GetProduct(2).GetQuantity());

            shoppingCart.RemoveProduct(1, removeQuantity1);
            shoppingCart.RemoveProduct(2, removeQuantity2);

            // Test removing more of an item than its quantity
            Assert.Equal(0, shoppingCart.GetProduct(1).GetQuantity());

            // Test normal removal
            Assert.Equal( (quantity2 - removeQuantity2), shoppingCart.GetProduct(2).GetQuantity());
        }

        [Fact]
        public void GetTotal_ShouldGetTheCorrectTotalOfAllCartItems()
        {
            Customer customer = new Customer();
            ShoppingCart shoppingCart = new ShoppingCart(customer);
            Product product1 = new Product();
            Product product2 = new Product();
            Product product3 = new Product();
            int quantity1 = 1;
            int quantity2 = 2;
            int quantity3 = 3;
            decimal price1 = 1.01m;
            decimal price2 = 2.02m;
            decimal price3 = 3.03m;

            product1.SetId(1);
            product1.SetPrice(price1);

            product2.SetId(2);
            product2.SetPrice(price2);

            product3.SetId(3);
            product3.SetPrice(price3);

            shoppingCart.AddProduct(product1);
            shoppingCart.AddProduct(product2, quantity2);
            shoppingCart.AddProduct(product3, quantity3);

            decimal correctCost = (quantity1 * price1) + (quantity2 * price2) + (quantity3 * price3);

            Assert.Equal(correctCost, shoppingCart.GetTotal());
            
        }

        [Fact]
        public void RemoveProduct_ShouldRemoveButReturnEmptyProduct()
        {
            Customer customer = new Customer();
            ShoppingCart shoppingCart = new ShoppingCart(customer);
            Product product1 = new Product();
            int quantity1 = 1;
            int removeQuantity1 = 5;

            product1.SetId(1);

            shoppingCart.AddProduct(product1, quantity1);

             // This removes more than what is in the cart

            // Test removing more of an item than its quantity
            Assert.Null(shoppingCart.RemoveProduct(1, removeQuantity1));

        }

        [Fact]
        public void FindStoreItemById_ShouldReturnCorrectStoreItem()
        {
            Customer customer = new Customer();
            ShoppingCart shoppingCart = new ShoppingCart(customer);
            Product product1 = new Product();
            ShoppingCartItem shCartItem;
            int quantity1 = 11;
            int idNumber = 1;

            product1.SetId(idNumber);
            shCartItem = shoppingCart.AddProduct(product1, quantity1);

            Assert.Equal(shCartItem, shoppingCart.GetProductById(idNumber));
        }

        [Fact]
        public void FindStoreItemById_ShouldReturnEmptyStoreItem()
        {
            Customer customer = new Customer();
            ShoppingCart shoppingCart = new ShoppingCart(customer);
            Product product1 = new Product();
            int quantity1 = 11;
            int idNumber = 1;

            product1.SetId(idNumber);
            shoppingCart.AddProduct(product1, quantity1);

            Assert.Null(shoppingCart.GetProductById(idNumber + 1));
        }
    }
}