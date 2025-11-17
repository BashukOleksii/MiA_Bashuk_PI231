using CRUDAPI.Models;

namespace CRUDAPI.Data
{
    public static class Elements
    {
        public static List<PeopleItems> peopleItems = new List<PeopleItems>()
                {
                    new PeopleItems() {Id = 1, Name = "Kate", Email = "Kate@Email.com" },
                    new PeopleItems() {Id = 2, Name = "John", Email = "John@Email.com" },
                    new PeopleItems() {Id = 3, Name = "Jane", Email = "Jane@Email.com" },
                    new PeopleItems() {Id = 4, Name = "Steve", Email = "Steve@Email.com" },
                    new PeopleItems() {Id = 5, Name = "Alex", Email = "Alex@Email.com" }
                };
        public static List<SubscriptionItems> subsripptionItems = new List<SubscriptionItems>()
                {
                    new SubscriptionItems(){Id = 1, OwnerId = 1, Service = "Netflix"},
                    new SubscriptionItems(){Id = 2, OwnerId = 1, Service = "Xbox"},
                    new SubscriptionItems(){Id = 3, OwnerId = 1, Service = "Amazon"},

                    new SubscriptionItems(){Id = 4, OwnerId = 2, Service = "Steam"},
                    new SubscriptionItems(){Id = 5, OwnerId = 2, Service = "Google"},
                    new SubscriptionItems(){Id = 6, OwnerId = 2, Service = "Amazon"},

                    new SubscriptionItems(){Id = 7, OwnerId = 3, Service = "YouTube"},
                    new SubscriptionItems(){Id = 8, OwnerId = 3, Service = "Netflix"},

                    new SubscriptionItems(){Id = 9, OwnerId = 4, Service = "YouTube"},
                    new SubscriptionItems(){Id = 10, OwnerId = 4, Service = "Netflix"},
                    new SubscriptionItems(){Id = 11, OwnerId = 4, Service = "Megogo"},
                    new SubscriptionItems(){Id = 12, OwnerId = 4, Service = "AppleFilms"},

                    new SubscriptionItems(){Id = 13, OwnerId = 5, Service = "AppleSerice"},
                    new SubscriptionItems(){Id = 14, OwnerId = 5, Service = "Xbox"},
                    new SubscriptionItems(){Id = 15, OwnerId = 5, Service = "AppleFilms"},

                };
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

            new MessageItems(){Id = 10,Title = "До оплаи підписки тиждень", OwnerId = 5, SubId = 15 },

        };
    }
}
