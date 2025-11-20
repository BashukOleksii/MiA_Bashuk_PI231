    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Task1.Abstract;
    using Task1.Models;

    namespace Task1.Handlers
    {
        public class UserBlockHandler : SubHandler
        {
            public override void Handle(User user, Sub sub)
            {
                if(user.IsBlocked)
                {
                    Console.WriteLine("Користувач заблокований");
                    return;
                }

                Console.WriteLine("Користувач не заблокований");

                if(Next is not null)
                    Next.Handle(user, sub);

            }
        }
    }
