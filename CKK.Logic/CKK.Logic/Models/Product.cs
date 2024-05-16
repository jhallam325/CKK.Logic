using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Models
{
    [Serializable]
    public class Product //: Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // What's the difference in P/p rice?
        private decimal price;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        /* 
         * No more verification?
        private decimal price;
        public decimal Price {
            get
            {
                return price;
            }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                price = value;
            } 
        }
        */
    }
}