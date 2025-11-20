using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Interfaces;

namespace Task2.Decorators
{

    public class PremiumSubDecorator : SubDecorator
    {
        public PremiumSubDecorator(ISub sub) : base(sub) { }

        public override int GetCountPeople() => _sub.GetCountPeople() + 10;

        public override double GetCost() => _sub.GetCost() + 1250;

        public override string GetFeatures() => _sub.GetFeatures() + "\nPremium features";

    }
}
