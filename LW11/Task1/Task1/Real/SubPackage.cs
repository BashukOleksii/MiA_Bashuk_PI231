using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Abstract;

namespace Task1.Real
{
    public class SubPackage : SubscriptionComponent
    {
        private List<SubscriptionComponent> _subs;
        public SubPackage(string name) : base(name) 
        {
            _subs = new List<SubscriptionComponent>(); 
        }

        
        
        
        
        public override double GetTotalPrice()
        {
            double totalPrice = 0;

            foreach (var sub in _subs)
                totalPrice += sub.GetTotalPrice();
            
            return totalPrice;
        }

        public override void Print(int indent = 0)
        {
            Console.WriteLine($"{new string(' ', indent)}+ Пакет: {Name}");

            foreach (var sub in _subs)
                sub.Print(indent + 2);

            Console.WriteLine($"{new string(' ', indent)} Ціна пакету: {GetTotalPrice()}");
        }




        public void Add(SubscriptionComponent sub)
        {
            if (sub is null)
                throw new ArgumentNullException();

            _subs.Add(sub);
        }

        public void Remove(SubscriptionComponent sub)
        {
            if (sub is null)
                throw new ArgumentNullException();

            if (!_subs.Contains(sub))
                throw new ArgumentException();

            _subs.Remove(sub);
        }



    }
}
