using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3_task_5.Models
{
    public class Country
    {
        public Name name { get; set; }
        public List<string> capital { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public double area { get; set; }
        public long population { get; set; }
        public Flag flags { get; set; }

    }

    public class Name
    {
        public string common { get; set; }
        public string official { get; set; }
    }

    public class Flag
    {
        public string png { get; set; }
        public string alt { get; set; }
    }
}
