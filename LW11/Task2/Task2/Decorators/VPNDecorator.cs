using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Interfaces;

namespace Task2.Decorators
{
    public class VPNDecorator: SubDecorator
    {
        public VPNDecorator(ISub sub) : base(sub) { }

        public override int GetCountPeople() => _sub.GetCountPeople() + 2;

        public override double GetCost() => _sub.GetCost() + 250;

        public override string GetFeatures() => _sub.GetFeatures() + "\nVPN";
     
    }
}
