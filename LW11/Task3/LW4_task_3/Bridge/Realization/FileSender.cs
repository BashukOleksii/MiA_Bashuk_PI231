using LW4_task_3.Models.Entities;

namespace LW4_task_3.Bridge.Realization
{
    public class FileSender : IMessageSender
    {
        private const string path = "Messages.log";
        public async Task SendAsync(MessageItem message)
        {
            string text = $"{message.OwnerId}: {message.Title}-{message.SubId}\n";

            await File.AppendAllTextAsync(path, text);
        }
    }
}
