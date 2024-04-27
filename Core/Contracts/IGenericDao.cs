namespace Core.Contracts;

public interface IGenericDao<T>
{
    public Task<IList<T>> GetAllAsync();

    public Task<T> GetByIdAsync(int id);

    public Task<T> AddAsync(T entity);

    public Task<T> UpdateAsync(T entity);

    public Task DeleteAsync(int id);

    public Task<bool> ExistsAsync(int id);

    public Task<int> CountAsync();
}
