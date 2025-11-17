using LW4_task_3.Models.Entities;

namespace LW4_task_3.Interface.InterfacesRepository
{
    public interface IUserRepository : IRepository<UserItem>
    {
        Task<List<UserItem>> GetAllAsync();
        Task<UserItem> GetByEmailAsync(string email);
    }
}
