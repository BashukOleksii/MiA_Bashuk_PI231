using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Models;

namespace Task2.Parts
{
    public class Notification
    {
        public void Send(User user, string message)
        {
            Console.WriteLine($"Повідомдення до {user.Name}: {message}");
        }
    }
}
