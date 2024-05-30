using CKK.Logic.Models;

namespace CKK.DB.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Order GetOrderByCustomerId(int id);
        Order GetById(int id);
    }
}
