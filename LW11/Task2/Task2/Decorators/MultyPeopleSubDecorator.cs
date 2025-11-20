using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Interfaces;

namespace Task2.Decorators
{

    public class MultyPeopleSubDecorator : SubDecorator
    {
        public MultyPeopleSubDecorator(ISub sub) : base(sub) { }

        public override int GetCountPeople() => _sub.GetCountPeople() + 20;

        public override double GetCost() => _sub.GetCost() + 1500;

        public override string GetFeatures() => _sub.GetFeatures() + "\nMulty people feature";

    }
}
