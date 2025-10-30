    namespace LW4_task_3.Models.Response
    {
        /// <summary>
        /// Моедль що представляє користувачів програми для управління підписками (Користувачі, які отримує клієнт)
        /// </summary>
        public class PeopleResponse
        {
            /// <summary>
            /// Унікальний ідентифікатор
            /// </summary>
            public string? Id { get; set; }
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
