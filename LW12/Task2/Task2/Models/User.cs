using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
    public class User
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public List<Sub> subs { get;} = new List<Sub>();

        public User(string name, double balance) 
        {
            Name = name;
            Balance = balance;
        }
    }
}
