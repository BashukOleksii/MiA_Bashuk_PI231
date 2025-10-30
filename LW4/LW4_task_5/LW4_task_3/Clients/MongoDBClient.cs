using MongoDB.Driver;

namespace LW4_task_3.Clients
{
    public class MongoDBClient
    {
        private static IMongoDatabase _database;
        private static MongoDBClient _instance;

        public static MongoDBClient Instance
        {
            get => _instance?? new MongoDBClient();
        }

        public MongoDBClient()
        {
            var conectionString = "mongodb+srv://bashuk0325oleksij_db_user:lZHXFstos2k8lAMX@data.t7bzerb.mongodb.net/?retryWrites=true&w=majority&appName=Data";
            var client = new MongoClient(conectionString);
            _database = client.GetDatabase("Elements");
        }

        public IMongoCollection<T> GetCollection<T>(string name)=> _database.GetCollection<T>(name);
    }
}
