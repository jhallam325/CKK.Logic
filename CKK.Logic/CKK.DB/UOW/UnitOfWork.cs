using CKK.DB.Interfaces;
using CKK.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.DB.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IConnectionFactory Connection)
        {
            Products = new ProductRepository(Connection);
            Orders = new OrderRepository(Connection);
            ShoppingCarts = new ShoppingCartRepository(Connection);
        }
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public IShoppingCartRepository ShoppingCarts { get; set; }
    }
}
