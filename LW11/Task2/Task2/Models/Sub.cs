using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Interfaces;

namespace Task2.Models
{
    public class StandartSub : ISub
    {
        public double GetCost() => 500;


        public int GetCountPeople() => 5;


        public string GetFeatures() => "Standart features";
        
    }
}
