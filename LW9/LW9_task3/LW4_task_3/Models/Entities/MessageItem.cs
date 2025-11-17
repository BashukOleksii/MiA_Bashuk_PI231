using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LW4_task_3.Models.Entities
{
    /// <summary>
    /// Модель реалізує сповіщення, кі отримує користувач про підписку
    /// </summary>
    public class MessageItem
    {
        /// <summary>
        /// Унікальний ідентифікатор повідомлення
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        /// <summary>
        /// Заголовок повідомлення
        /// </summary>
        [BsonElement("title")]
        public string? Title { get; set; }
        /// <summary>
        /// Кому надсилаєься повідомлення
        /// </summary>
        [BsonElement("ownerId")]
        public string? OwnerId { get; set; }
        /// <summary>
        /// Індетифікатор підписки
        /// </summary>
        [BsonElement("subId")]
        public string? SubId { get; set; }

    }
}
