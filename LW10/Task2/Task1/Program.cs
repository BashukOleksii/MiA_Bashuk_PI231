
using System.Text;
using System.Text.Unicode;
using Task1;
using Task1.Model;
using Task1.Services;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        int num;

        SubLogger.Instance.Log("Початок запуску програми");

        do
        {
            num = -1;
            Console.WriteLine("Введіть дію, яку бажаєте здійснити" +
                "\n1 - Додати підписку" +
                "\n2 - Видалити підписку" +
                "\n3 - Переглянуи всі підписки" +
                "\n4 - Створити на основі існуючої" + 
                "\nІнше - вихід");

            int.TryParse(Console.ReadLine(), out num);

            switch (num)
            {
                case 1:
                    {
                        Subscription subscription = new Subscription();
                        subscription.Init();
                        SubService.Instance.AddSub(subscription);
                    }break;
                case 2:
                    {
                        try
                        {
                            Console.WriteLine("Введіть id: ");
                            string id = Console.ReadLine();
                            SubService.Instance.RemoveSub(id);
                        }
                        catch (ArgumentException aex)
                        {
                            Console.WriteLine(aex.Message);
                        }

                    }break;
                case 3:
                    {
                        try
                        {
                            Console.WriteLine("Вміст:");
                            SubService.Instance.Print();
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        
                    }break;
                case 4:
                    {
                        try
                        {
                            Console.WriteLine("Введіть id:");
                            string id = Console.ReadLine();
                            SubService.Instance.CreateFromExist(id);
                        }
                        catch(ArgumentException aex)
                        {
                            Console.WriteLine(aex.Message);    
                        }
                    }break;

            }



        } while (num >= 1 && num <= 4);

        SubLogger.Instance.Log("Кінець роботи програми");
    }

   
}

