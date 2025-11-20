using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Abstract;
using Task1.Models;

namespace Task1.Handlers
{
    public class FinalSteepHandler : SubHandler
    {
        public override void Handle(User user, Sub sub)
        {
            user.Balance -= sub.Cost;
            user.AddSub(sub);
            Console.WriteLine($"Підписка оформлена для користувача: {user.Name}");
        }
    }
}
