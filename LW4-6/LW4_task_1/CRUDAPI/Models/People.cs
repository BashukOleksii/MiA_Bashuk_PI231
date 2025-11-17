namespace CRUDAPI.Models
{
    /// <summary>
    /// Моедль що представляє користувачів програми для управління підписками
    /// </summary>
    public class PeopleItems
    {
        /// <summary>
        /// Унікальний ідентифікатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Ім'я або логін користувача
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Поштова адреса користувача
        /// </summary>
        public string Email { get; set; }
    }
}
