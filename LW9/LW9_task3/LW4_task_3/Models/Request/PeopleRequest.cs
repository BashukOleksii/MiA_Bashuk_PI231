namespace LW4_task_3.Models.Request
{
    /// <summary>
    /// Моедль що представляє користувачів програми для управління підписками (Користувачі, кі відправляє клієнт)
    /// </summary>
    public class PeopleRequest
    {
        /// <summary>
        /// Ім'я або логін користувача
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Поштова адреса користувача
        /// </summary>
        public string? Email { get; set; }
        
    }
}
