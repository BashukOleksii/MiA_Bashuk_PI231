namespace LW4_task_3.Models.Request
{

    /// <summary>
    /// Модель, яка описує підпску, яку може мати користувач (Підписки, які відправляє клієнт)
    /// </summary>
    public class SubscriptionRequest
    {
       
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
