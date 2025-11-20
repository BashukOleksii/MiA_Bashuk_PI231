using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Abstract;
using Task1.Models;

namespace Task1.Handlers
{
    public class BalanceHandler : SubHandler
    {
        public override void Handle(User user, Sub sub)
        {
            if(user.Balance < sub.Cost)
            {
                Console.WriteLine("Недостатньо коштів");
                return;
            }

            Console.WriteLine("Баланс відповідає потребам");
            if(Next is not null)
                 Next.Handle(user, sub);

        }
    }
}
