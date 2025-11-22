namespace LW4_task_3.Models.Response
{
    /// <summary>
    /// Модель, яка описує підпску, яку може мати користувач (Підписка, яку отримує клієнт)
    /// </summary>
    public class SubscriptionResponse
    {
        /// <summary>
        /// Унікальний індентифікатор підписки
        /// </summary>

        public string? Id { get; set; }

        /// <summary>
        /// Ідентиікатор власника пдписки
        /// </summary>
        public string? OwnerId { get; set; }

        /// <summary>
        /// Сервіс для кого виконана підпска
        /// </summary>
        public string? Service { get; set; }
        /// <summary>
        /// Статус в якому перебуваєпідписка користувача
        /// </summary>
        public string? Status { get; set; }
    }
}
