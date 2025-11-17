using MongoDB.Bson;

namespace LW4_task_3.Validators
{
    public class ValidElement
    {
        public static void ValidId(string id)
        {
            if (!ObjectId.TryParse(id, out var ID))
                throw new ArgumentException($"Не коректний формат для id: {id}");
        }
    }
}
