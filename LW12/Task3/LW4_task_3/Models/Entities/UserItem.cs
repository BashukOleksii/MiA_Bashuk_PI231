using LW4_task_3.Enums;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace LW4_task_3.Models.Entities
{
    public class UserItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("username")]
        public string? UserName { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("paswordHash")]
        public string? Password { get; set; }

        [BsonElement("refreshToken")]
        public string? RefreshToken { get; set;}

        [BsonElement("refreshTokenExpireTime")]
        public DateTime? RefreshTokenExpireTime { get; set; }

        [BsonElement("role")]
        public UserRole Role { get; set; } = UserRole.User;

    }
}
