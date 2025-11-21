namespace LW4_task_3.Interface.InterfacesRepository
{
    public interface IRepository<T>
    {
        Task CreateAsync(T element);
        Task<T> GetByIdAsync(string id);
        Task DeleteAsync(string id);
        Task UpdateAsync(string id, T element);
        Task<bool> IsExist(string id);
    }
}
