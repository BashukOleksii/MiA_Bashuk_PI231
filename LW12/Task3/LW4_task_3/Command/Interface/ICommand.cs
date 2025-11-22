namespace LW4_task_3.Command.Interface
{
    public interface IPeopleCommand
    {
        Task ExecuteAsync();
        Task Undo();
    }
}
