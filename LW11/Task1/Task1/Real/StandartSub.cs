using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Task1.Abstract;

namespace Task1.Real
{
    public class StandartSub : SubscriptionComponent
    {
        public double Price { get; set; }
        public StandartSub(string name, double price) : base(name) 
        {
            Price = price;
        }

        public override double GetTotalPrice() => 
            Price;

        public override void Print(int indent = 0) =>
            Console.WriteLine($"{new string(' ', indent)}- {Name}: {Price} грн.");
        
    }
}
