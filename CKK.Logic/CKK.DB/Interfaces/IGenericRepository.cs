namespace CKK.DB.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(int id);
        List<T> GetAll();
        int Add(T entity);
        int Update(T entity);
        int Delete(int id);
    }
}
