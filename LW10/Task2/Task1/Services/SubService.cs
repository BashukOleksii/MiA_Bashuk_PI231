using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Model;

namespace Task1.Services
{
    public class SubService
    {
        private static SubService _instance;
        private List<Subscription> subscriptions;

        private SubService()
        {
            subscriptions = new List<Subscription>();
            SubLogger.Instance.Log("Створено сервіс");
        }

        public static SubService Instance
        {
            get => _instance ?? (_instance = new SubService());
        } 

        public void AddSub(Subscription subscription)
        {
            subscriptions.Add(subscription);
            SubLogger.Instance.Log("Додано підписку: " + subscription);
        }

        public void RemoveSub(string id)
        {
            var sub = subscriptions.FirstOrDefault(s => s.Id == id);

            if (sub is null)
            {
                SubLogger.Instance.Log($"Помилка отримання підписки за id: {id}");
                throw new ArgumentException($"Не знайдено за id: {id}");
            }
            subscriptions.Remove(sub);

            SubLogger.Instance.Log($"Видалено підписку: {sub}");
        }

        public void Print()
        {
            if (subscriptions.Count == 0)
            {
                SubLogger.Instance.Log("Споба переглянути порожній список");
                throw new Exception("Список пустий");
            }

            foreach (var subscription in subscriptions)
            {
                Console.WriteLine(subscription);
            }

            SubLogger.Instance.Log("Переглянуто вміст списку");

        }

        public void CreateFromExist(string id)
        {
            var sub = subscriptions.FirstOrDefault(s => s.Id == id);

            if(sub is null)
            {
                SubLogger.Instance.Log("Не знайдено підписку при спробі копіювання");
                throw new ArgumentException($"Не знайдено підписку із вказаним id:{id}");
            }

            subscriptions.Add((Subscription)sub.Clone());
        }

    }
}
