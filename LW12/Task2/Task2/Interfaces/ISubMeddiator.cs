using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Models;

namespace Task2.Interfaces
{
    public interface ISubMeddiator
    {
        public void Subscribe(User user, Sub sub);
    }
}
