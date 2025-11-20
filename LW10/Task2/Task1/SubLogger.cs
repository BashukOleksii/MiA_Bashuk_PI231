using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class SubLogger
    {
        private static SubLogger _instance;
        private string path;

        public static SubLogger Instance
        {
            get => _instance ?? (_instance = new SubLogger());
        }

        private SubLogger()
        {
            path = "SubscriptionLog.log";
            Log("Створено об'єк логгера");
        }

        public void Log(string message)
        {
            string str = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - LOG: {message}" + Environment.NewLine;
            File.AppendAllText(path, str);
        }
        



    }
}
