﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    // Does this need to be serializable?
    [Serializable]
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
