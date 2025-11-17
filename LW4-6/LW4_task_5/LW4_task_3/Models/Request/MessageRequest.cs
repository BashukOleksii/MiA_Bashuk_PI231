namespace LW4_task_3.Models.Request
{
    /// <summary>
    /// Модель реалізує сповіщення, кі отримує користувач про підписку (Сповіщення, які відправляє клієнт)
    /// </summary>
    public class MessageRequest
    {
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
