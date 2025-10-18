namespace LW_4_2.Models
{
    /// <summary>
    /// Модель реалізує сповіщення, кі отримує користувач про підписку
    /// </summary>
    public class MessageItems
    {
        /// <summary>
        /// Унікальний ідентифікатор повідомлення
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Заголовок повідомлення
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Кому надсилаєься повідомлення
        /// </summary>
        public int? OwnerId { get; set; }
        /// <summary>
        /// Індетифікатор підписки
        /// </summary>
        public int? SubId { get; set; }

    }
}