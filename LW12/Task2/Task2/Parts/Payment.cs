using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Models;

namespace Task2.Parts
{
    public class Payment
    {
        public bool Pay(User user, Sub sub)
        {
            if(user.Balance < sub.Price)
                return false;

            user.Balance -= sub.Price;
            return true;
        }

    }
}
