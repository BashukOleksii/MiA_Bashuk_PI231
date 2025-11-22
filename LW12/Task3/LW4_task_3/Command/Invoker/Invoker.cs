using LW4_task_3.Command.Interface;

namespace LW4_task_3.Command.Invoker
{
    public class PeopleInvoker
    {
        private Stack<IPeopleCommand> _commands = new Stack<IPeopleCommand>();

        public async Task ExecuteAsync(IPeopleCommand command)
        {
            await command.ExecuteAsync();

            _commands.Push(command);
        }

        public async Task UndoAsync()
        {
            if (_commands.Count > 0)
            {
                var c = _commands.Pop();

                await c.Undo();
            }
        }

    }
}
