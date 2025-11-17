using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LW4_task_3.Models.Entities
{
    /// <summary>
    /// Перелік статусів підписки
    /// </summary>
    public enum SubStatus
    {
        /// <summary>
        /// Статус очікування - Expectation - 1
        /// </summary>
        Expectation = 1, 
        /// <summary>
        /// Статус ативна - Active - 2
        /// </summary>
        Active,
        /// <summary>
        /// Статус прострочена - Overdue - 3
        /// </summary>
        Overdue,
        /// <summary>
        /// Статус Архівована - Archived - 4
        /// </summary>
        Archived
    }
    /// <summary>
    /// Модель, яка описує підпску, яку може мати користувач
    /// </summary>
    public class SubscriptionItem
    {
        /// <summary>
        /// Унікальний індентифікатор підписки
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// Ідентиікатор власника пдписки
        /// </summary>
        [BsonElement("ownerId")]
        public string? OwnerId { get; set; }

        /// <summary>
        /// Сервіс для кого виконана підпска
        /// </summary>
        [BsonElement("service")]
        public string? Service { get; set; }
        /// <summary>
        /// Статус в якому перебуваєпідписка користувача
        /// </summary>
        [BsonElement("status")]
        [BsonRepresentation(BsonType.Int32)]
        public SubStatus Status { get; set; }
    }
}
