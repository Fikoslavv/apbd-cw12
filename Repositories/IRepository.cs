namespace apbd_cw12.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> GetQueryable();
    Task<T> GetByIdAsync(params object[] keys);
    Task<T> InsertAsync(T obj);
    Task<T> UpdateAsync(T obj);
    Task DeleteAsync(params object[] keys);
}
