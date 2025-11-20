using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Interfaces;

namespace Task2.Decorators
{
    public class SubDecorator: ISub
    {
        protected readonly ISub _sub;

        public SubDecorator(ISub sub)
        {
            _sub = sub;
        }

        public virtual double GetCost() =>
            _sub.GetCost();
        

        public virtual int GetCountPeople() => 
            _sub.GetCountPeople();
       

        public virtual string GetFeatures() =>
            _sub.GetFeatures();
        
    }
}
