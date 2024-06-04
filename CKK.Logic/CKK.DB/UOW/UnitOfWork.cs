using CKK.DB.Interfaces;
using CKK.DB.Repository;

namespace CKK.DB.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IConnectionFactory connection)
        {
            Products = new ProductRepository(connection);
            Orders = new OrderRepository(connection);
            ShoppingCarts = new ShoppingCartRepository(connection);
        }
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public IShoppingCartRepository ShoppingCarts { get; set; }
    }
}
