namespace LW4_task_3.Models.Response
{
    /// <summary>
    /// Модель реалізує сповіщення, кі отримує користувач про підписку (Відповідь клієнту)
    /// </summary>
    public class MessageResponse
    {
        /// <summary>
        /// Унікальний ідентифікатор повідомлення
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Заголовок повідомлення
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Кому надсилаєься повідомлення
        /// </summary>
        public string? OwnerId { get; set; }
        /// <summary>
        /// Індетифікатор підписки
        /// </summary>
        public string? SubId { get; set; }

    }
}
