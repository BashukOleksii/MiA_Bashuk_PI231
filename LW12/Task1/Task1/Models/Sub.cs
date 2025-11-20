    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Task1.Models
    {
        public class Sub
        {
            public string Service {  get; set; }
            public string Type { get; set; }
            public double Cost { get; set; }

            public Sub(string service, string type, double cost)
            {
                Service = service;
                Type = type;
                Cost = cost;
            }
        }
    }
