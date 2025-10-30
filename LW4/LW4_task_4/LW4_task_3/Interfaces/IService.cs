namespace LW4_task_3.Interfaces
{
    public interface IService<T> 
    {
        Task CreateAsync(T element);
        Task<T> GetByIdAsync(string id);
        Task DeleteAsync(string id);
        Task UpdateAsync(string id, T element);
    }
}
