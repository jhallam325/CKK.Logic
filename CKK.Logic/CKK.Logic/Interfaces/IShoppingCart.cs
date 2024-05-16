using CKK.Logic.Models;

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/

namespace CKK.Logic.Interfaces
{
    public interface IShoppingCart
    {
        public int GetCustomerId();
        public ShoppingCartItem? AddProduct(ShoppingCartItem product, int quantity);
        public ShoppingCartItem? RemoveProduct(int id, int quantity);
        //public decimal GetTotal();
        public ShoppingCartItem? GetProductById(int id);
        public List<ShoppingCartItem>? GetProducts();
    }
}
