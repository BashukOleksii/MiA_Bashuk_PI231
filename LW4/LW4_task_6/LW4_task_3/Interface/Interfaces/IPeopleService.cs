using LW4_task_3.Models.Entities;

namespace LW4_task_3.Interface.Interfaces
{
    public interface IPeopleService: IService<PeopleItem>
    {
        Task<IEnumerable<PeopleItem>?> GetPeoplesItemsAsync(string? name, string? email);
    }
}
