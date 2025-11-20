using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Abstract;
using Task1.Models;

namespace Task1.Handlers
{
    public class SubTypeHandler : SubHandler
    {
        public override void Handle(User user, Sub sub)
        {
            if(sub.Type != "Standart" || sub.Type == "Premium")
            {
                Console.WriteLine($"Невірний тип підписки: {sub.Type}");
                return;
            }

            Console.WriteLine("Вірний тип підписки");
            if(Next is not null)
                Next.Handle(user, sub);

        }
    }
}
