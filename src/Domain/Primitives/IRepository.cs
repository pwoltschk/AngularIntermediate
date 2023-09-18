namespace Domain.Primitives;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task RemoveAsync(T entity);
    Task UpdateAsync(T entity);
}