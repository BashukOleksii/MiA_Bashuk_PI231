using System.Text;
using Task2;
using Task2.Interfaces;
using Task2.Models;
using Task2.Parts;

public class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        User user = new User("User1", 1000);
        Sub sub = new Sub("Netflix", 500);

        ISubMeddiator subMeddiator = new SubMediator(new Payment(), new Notification());

        subMeddiator.Subscribe(user, sub);
    }
}