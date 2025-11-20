using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Abstract
{
    public abstract class SubscriptionComponent
    {
        public string Name { get; set;  }

        public SubscriptionComponent(string name)
        {
            Name = name;
        }

        public abstract double GetTotalPrice();
        public abstract void Print(int indent = 0);
    }
}
