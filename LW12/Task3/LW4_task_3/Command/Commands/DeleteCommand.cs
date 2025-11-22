using LW4_task_3.Command.Interface;
using LW4_task_3.Interface.Interfaces;
using LW4_task_3.Models.Entities;

namespace LW4_task_3.Command.Commands
{
    public class DeleteCommand : IPeopleCommand
    {
        private readonly IPeopleService _service;
        private readonly string _id;
        private PeopleItem _peopleItem;

        public DeleteCommand(IPeopleService service, string id)
        {
            _service = service;
            _id = id;
        }

        public async Task ExecuteAsync()
        {
            _peopleItem = await _service.GetByIdAsync(_id);

            await _service.DeleteAsync(_id);

        }

        public async Task Undo() =>
            await _service.CreateAsync(_peopleItem);
        
    }
}
