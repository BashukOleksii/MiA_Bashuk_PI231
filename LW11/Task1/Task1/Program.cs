using System.Text;
using Task1.Abstract;
using Task1.Real;

class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        SubPackage FilmPackage = new SubPackage("Фільми");
            SubscriptionComponent sub1 = new StandartSub("Netflix", 1000);
            SubscriptionComponent sub2 = new StandartSub("Megogo", 550);
            SubscriptionComponent sub3 = new StandartSub("AppleTV", 750);
        FilmPackage.Add(sub1);
        FilmPackage.Add(sub2);
        FilmPackage.Add(sub3);

        SubPackage GooglePackage = new SubPackage("Google");
            SubscriptionComponent sub4 = new StandartSub("YouTube", 1000);
            SubscriptionComponent sub5 = new StandartSub("YouTubeMusic", 500);
            SubscriptionComponent sub6 = new StandartSub("OneDrive", 1200);
        GooglePackage.Add(sub4);
        GooglePackage.Add(sub5);
        GooglePackage.Add(sub6);

        SubPackage GamePackage = new SubPackage("Game");
            SubscriptionComponent sub7 = new StandartSub("PlayStation", 1500);
            SubscriptionComponent sub8 = new StandartSub("XBox", 1200);
            SubscriptionComponent sub9 = new StandartSub("Nintendo", 1325);
        GamePackage.Add(sub7);
        GamePackage.Add(sub8);
        GamePackage.Add(sub9);

        SubPackage StandartPackage = new SubPackage("Стандартний");
            StandartPackage.Add(FilmPackage);
            StandartPackage.Add(GamePackage);
            StandartPackage.Add(new StandartSub("Київстар", 150));

        SubPackage MegaPackage = new SubPackage("Mega");
            MegaPackage.Add(StandartPackage);
            MegaPackage.Add(GooglePackage);
            MegaPackage.Add(new StandartSub("Car", 10000));

        MegaPackage.Print();


    }
}