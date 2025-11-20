using System.Net.Http.Headers;
using System.Text;
using Task2.Decorators;
using Task2.Interfaces;
using Task2.Models;

public class Prorgam
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        ISub subscription = new StandartSub();
        Console.WriteLine("Стандартна підписка: ");
        subInfo(subscription);

        subscription = new VPNDecorator(subscription);
        Console.WriteLine("Стандартна підписка + VPN: ");
        subInfo(subscription);

        subscription = new PremiumSubDecorator(subscription);
        Console.WriteLine("Стандартна підписка + VPN + Premium: ");
        subInfo(subscription);

        subscription = new MultyPeopleSubDecorator(subscription);
        Console.WriteLine("Стандартна підписка + VPN + Premium + Multi: ");
        subInfo(subscription);


    }

    private static void subInfo(ISub subscription)
    {
        Console.WriteLine("Функції:");
        Console.WriteLine(subscription.GetFeatures());
        Console.WriteLine($"Кількість людей в підписці: {subscription.GetCountPeople()}");
        Console.WriteLine($"Вартість підписки: {subscription.GetCost()}");

        Console.WriteLine();
    }
}