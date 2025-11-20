using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1.Abstract
{
    public abstract class SubHandler
    {
        protected SubHandler Next;

        public SubHandler SetNext(SubHandler next)
        {
            Next = next; 
            return next;
        }

        public abstract void Handle(User user, Sub sub);
    }
}
