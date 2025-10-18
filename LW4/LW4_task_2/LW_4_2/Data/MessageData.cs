using LW_4_2.Models;

namespace LW_4_2.Data
{
    public static class MessageData
    {
        public static List<MessageItems> messageItems = new List<MessageItems>()
        {
            new MessageItems(){Id = 1,Title = "До оплати три дні", OwnerId = 1, SubId = 1 },
            new MessageItems(){Id = 2,Title = "До оплати два дні", OwnerId = 1, SubId = 1 },
            new MessageItems(){Id = 3,Title = "До оплати один день", OwnerId = 1, SubId = 1 },

            new MessageItems(){Id = 4,Title = "Підписка прострочена", OwnerId = 2, SubId = 5 },

            new MessageItems(){Id = 5,Title = "Підписку скасовано", OwnerId = 3, SubId = 7 },

            new MessageItems(){Id = 6,Title = "Підписку просрочена", OwnerId = 4, SubId = 9 },
            new MessageItems(){Id = 7,Title = "Підпску продовжено", OwnerId = 4, SubId = 9 },
            new MessageItems(){Id = 8,Title = "Підписка прострочена", OwnerId = 4, SubId = 11 },
            new MessageItems(){Id = 9,Title = "Підписку скасовано", OwnerId = 4, SubId = 11 },

            new MessageItems(){Id = 10,Title = "До оплати підписки тиждень", OwnerId = 5, SubId = 15 },

        };
    }
}       
