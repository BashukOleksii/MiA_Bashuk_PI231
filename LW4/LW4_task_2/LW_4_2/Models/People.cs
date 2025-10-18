using System.ComponentModel.DataAnnotations;

namespace LW_4_2.Models
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
        [Required(ErrorMessage = "Поле 'Name' Обов'язкове для заповнення")]
        [RegularExpression(@"^[A-Z][a-z]{5,10}[0-9]{0,5}$", ErrorMessage = "'Name' не відповідає вимогам:" +
            "Перші велика англійська літера, далі від 5 до 10 малих англійських символів та цифри за бажанням")]
        public string? Name { get; set; }
        /// <summary>
        /// Поштова адреса користувача
        /// </summary>
        [Required(ErrorMessage = "Поле 'Email' є обов'язковим")]
        [EmailAddress(ErrorMessage = "Не коректне значення 'Email'")]
        public string? Email { get; set; }
    }
}