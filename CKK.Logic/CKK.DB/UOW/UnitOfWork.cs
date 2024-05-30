using CKK.DB.Interfaces;
using CKK.DB.Repository;

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
