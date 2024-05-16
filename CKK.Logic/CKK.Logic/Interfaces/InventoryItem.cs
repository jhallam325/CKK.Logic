using CKK.Logic.Models;
using CKK.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Interfaces
{
    [Serializable]
    public abstract class InventoryItem
    {
        private ShoppingCartItem product;
        private int quantity;

        public ShoppingCartItem Product { get; set; }
        public int Quantity {
            get
            {
                return quantity;
            }
            set
            {
                if (value < 0)
                {
                    throw new InventoryItemStockTooLowException();
                }

                quantity = value;
            }
        }
    }
}
