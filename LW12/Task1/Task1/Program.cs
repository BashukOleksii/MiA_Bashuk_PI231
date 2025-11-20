    using System.Text;
    using Task1.Abstract;
    using Task1.Handlers;
    using Task1.Models;

    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            User user = new User("User",1000,false);
            Sub sub = new Sub("Netflix","Standart",500);

            SubHandler handler = new UserBlockHandler();

            handler.SetNext(new BalanceHandler())
                .SetNext(new SubTypeHandler())
                .SetNext(new FinalSteepHandler());

            handler.Handle(user, sub);

            Console.WriteLine($"Результат додавання підписки: {user.subs[0].Service} присутня");
        
        }
    }