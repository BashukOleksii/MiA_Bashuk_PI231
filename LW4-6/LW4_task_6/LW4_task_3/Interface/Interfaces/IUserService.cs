using LW4_task_3.Models.Entities;

namespace LW4_task_3.Interface.Interfaces
{
    public interface IUserService: IService<UserItem>
    {
        Task<UserItem> GetByEmailAsync(string email);
        Task<List<UserItem>> GetAllAsync(); 
    }
}
