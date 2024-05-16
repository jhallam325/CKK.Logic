using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;
using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
    [Serializable]
    public class ShoppingCartItem //: InventoryItem
    {
        public Product Product { get; set; }
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        // What is the difference in these two Q/q uantity properties?
        private int quantity { get; set; }
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (value >= 0)
                {
                    quantity = value;
                }
                else
                {
                    throw new InventoryItemStockTooLowException();
                }
            }
        }

        // Do we still need the constructor?
        public ShoppingCartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal GetTotal()
        {
            return Quantity * Product.Price;
        }
    }
}