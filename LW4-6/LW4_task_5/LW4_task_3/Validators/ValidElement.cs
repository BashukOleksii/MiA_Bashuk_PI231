using LW4_task_3.Models.Entities;
using MongoDB.Bson;

namespace LW4_task_3.Validators
{
    public class ValidElement
    {
        public static void ValidId(string id)
        {
            if (!ObjectId.TryParse(id, out var ID))
                throw new KeyNotFoundException($"Не знайдено користувача за id: {id}");
        }

        public static bool Id(string id) =>
            ObjectId.TryParse(id, out var ID);


        public static bool ValidStatus(string status)=>
            Enum.TryParse<SubStatus>(status, true, out var result);
        
    }
}
