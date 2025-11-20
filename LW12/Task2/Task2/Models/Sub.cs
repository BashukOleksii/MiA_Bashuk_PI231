using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
    public class Sub
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Sub(string name, double price)
        {
            Name = name; Price = price;
        }
    }
}
