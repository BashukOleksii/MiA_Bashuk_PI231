namespace CRUDAPI.Models
{
    /// <summary>
    /// Модель, яка описує підпску, яку може мати користувач
    /// </summary>
    public class SubscriptionItems
    {
        /// <summary>
        /// Унікальний індентифікатор підписки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ідентиікатор власника пдписки
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Сервіс для кого виконана підпска
        /// </summary>
        public string Service { get; set; }
    }
}
