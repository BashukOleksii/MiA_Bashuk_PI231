using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Models
{
    public class User
    {
        public List<Sub> subs {  get; private set; }   
        public string Name { get; set; }
        public double Balance { get; set; }
        public bool IsBlocked { get; set; }
        public User(string name, double balance, bool isBlocked)
        {
            Name = name;
            Balance = balance;
            IsBlocked = isBlocked;

            subs = new List<Sub>();
        }

        public void AddSub(Sub sub) =>
            subs.Add(sub); 

        public void RemoveSub(Sub sub)
        {
            if (!subs.Contains(sub))
                throw new ArgumentException();

            subs.Remove(sub);
        }
    }
}
