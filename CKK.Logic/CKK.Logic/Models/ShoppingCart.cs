using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart
    {
        public Customer Customer { get; set; }
        public List<ShoppingCartItem> ShCartItems { get; set; }

        //* ShoppingCart(Customer cust) 
        public ShoppingCart(Customer customer)
        {
            Customer = customer;
        }

        //* GetCustomerId() : int
        //* Returns the customer's id 
        public int GetCustomerId()
        {
            return Customer.Id;
        }

        //* GetProductById(int id) : ShoppingCartItem
        //* Returns the cartItem with matching id.
        public ShoppingCartItem? GetProductById(int id)
        {
            foreach (ShoppingCartItem item in ShCartItems)
            {
                if (id == item.Product.Id)
                {
                    return item;
                }
            }
            return null;
        }

        //* AddProduct(Product prod, int quantity) : ShoppingCartItem
        //* Checks for valid quantity
        //* Checks for product and adds quantity if found
        //* Adds product if new
        //* Returns the cartItem changed or null
        //  In the AddProduct method, if the item already exists in the List,
        //  then you should add the quantity variable of that item and not add a new ShoppingCartItem.
        public ShoppingCartItem? AddProduct(Product product, int quantity)
        {
            // Checks for valid quantity
            if (quantity < 0 || product == null)
            {
                return null;
            }

            ShoppingCartItem newItem = new ShoppingCartItem(product, quantity);

            if (ShCartItems.Count() <= 0)
            {
                ShCartItems.Add(newItem);
                return newItem;
            }

            // Contains doesn't work since the quantity doesn't need to match
            foreach (ShoppingCartItem item in ShCartItems)
            {
                if (item.Product.Id == newItem.Product.Id)
                {
                    int indexOfItem = ShCartItems.IndexOf(item);
                    int oldQuantity = ShCartItems[indexOfItem].Quantity;
                    ShCartItems[indexOfItem].Quantity = oldQuantity + quantity;
                    return ShCartItems[indexOfItem];
                }
            }

            // quanity is > 0 and the new item was not found in _storeItems list
            ShCartItems.Add(newItem);
            return newItem;
        }

        //* RemoveProduct(Product prod, int quantity) : ShoppingCartItem
        //* Checks for valid quantity
        //* Checks for product and removes quantity if found
        //* Returns the product changed or null
        public ShoppingCartItem? RemoveProduct(int id, int quantity)
        {
            if (quantity <= 0)
            {
                return null;
            }

            ShoppingCartItem? oldItem = GetProductById(id);

            if (oldItem != null)
            {
                if (oldItem.Quantity - quantity <= 0)
                {
                    oldItem.Quantity = 0;
                    ShCartItems.Remove(oldItem);
                }
                else
                {
                    oldItem.Quantity = (oldItem.Quantity - quantity);
                }
                return oldItem;
            }
            else
            {
                Console.WriteLine($"The ID {id} is not a product in the " +
                    $"shoppingCartItems list");
                return null;
            }
        }

        //* GetTotal() : decimal
        //* returns total of all products
        public decimal GetTotal()
        {
            decimal total = 0m;
            foreach (ShoppingCartItem item in ShCartItems)
            {
                total += item.GetTotal();
            }
            return total;
        }

        //* GetProduct(int prodNum) : ShoppingCartItem
        //* returns the cartItem in the position of prodNum or null
        public ShoppingCartItem? GetProduct(int productNumber)
        {
            foreach (ShoppingCartItem item in ShCartItems)
            {
                if (productNumber == item.Product.Id)
                {
                    return item;
                }
            }
            return null;
        }

        public List<ShoppingCartItem>? GetProducts()
        {
            return ShCartItems;

        }

        public ShoppingCartItem? AddProduct(ShoppingCartItem product, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}