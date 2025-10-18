namespace LW_4_2.Models
{
    /// <summary>
    /// Перелік статусів підписки
    /// </summary>
    public enum SubStatus
    {
        /// <summary>
        /// Статус очікування - Expectation - 0
        /// </summary>
        Expectation,
        /// <summary>
        /// Статус ативна - Active - 1
        /// </summary>
        Active,
        /// <summary>
        /// Статус прострочена - Overdue - 2
        /// </summary>
        Overdue,
        /// <summary>
        /// Статус Архівована - Archived - 3
        /// </summary>
        Archived
    }
    /// <summary>
    /// Модель, яка описує підпску, яку може мати користувач
    /// </summary>
    public class SubscriptionItems
    {
        /// <summary>
        /// Унікальний індентифікатор підписки
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Ідентиікатор власника пдписки
        /// </summary>
        public int? OwnerId { get; set; }

        /// <summary>
        /// Сервіс для кого виконана підпска
        /// </summary>
        public string? Service { get; set; }
        /// <summary>
        /// Статус в якому перебуваєпідписка користувача
        /// </summary>
        public SubStatus? Status { get; set; }
    }
}