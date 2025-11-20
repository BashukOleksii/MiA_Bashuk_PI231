using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Subscription
    {
        public string Id { get; private set; }   
        public string Service { get; set; }
        public double Cost { get; set; }

        public Subscription(string service = "", double cost = 0)
        {
            Id = Guid.NewGuid().ToString().Substring(0,5);
            Service = service;
            Cost = cost;
        }

        public void Init()
        {
            do
            {
                Console.WriteLine("Введіть назву сервісу:");
                Service = Console.ReadLine();
            } while (string.IsNullOrEmpty(Service));

            Console.WriteLine("Введіть вартість");
            bool valid = false;
            double cost = 0;
            while (!valid)
                valid = double.TryParse(Console.ReadLine(), out cost) && cost > 0;
            
            Cost = cost;
        }
        public override string ToString() =>
            $"Підписка #{Id} - {Service}:{Cost}";
        
    }
}
