using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Interfaces;
using Task2.Models;
using Task2.Parts;

namespace Task2
{
    public class SubMediator : ISubMeddiator
    {
        private Payment _paymentService;
        private Notification _notification;

        public SubMediator(Payment payment, Notification notification)
        {
            _paymentService = payment;
            _notification = notification;
        }

        public void Subscribe(User user, Sub sub)
        {
            Logger.Instance.Log($"Користувач {user.Name} хоче оформити підписку {sub.Name}");

            if (!_paymentService.Pay(user, sub))
            {
                _notification.Send(user, "не достатньо коштів для оформлення підписки");
                Logger.Instance.Log("Помилка, недостатньо коштів");
                return; 
            }

            user.subs.Add(sub);

            _notification.Send(user, "підписку оформлено");
            Logger.Instance.Log("Успіх, підписка оформлена");

        }
    }
}
