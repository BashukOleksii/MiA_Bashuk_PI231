using LW_4_2.Models;

namespace LW_4_2.Data
{
    public static class SubData
    {
        public static List<SubscriptionItems> subsripptionItems = new List<SubscriptionItems>()
                {
                    new SubscriptionItems(){Id = 1, OwnerId = 1, Service = "Netflix", Status = SubStatus.Active},
                    new SubscriptionItems(){Id = 2, OwnerId = 1, Service = "Xbox",Status = SubStatus.Active},
                    new SubscriptionItems(){Id = 3, OwnerId = 1, Service = "Amazon",Status = SubStatus.Active},

                    new SubscriptionItems(){Id = 4, OwnerId = 2, Service = "Steam",Status = SubStatus.Active },
                    new SubscriptionItems(){Id = 5, OwnerId = 2, Service = "Google", Status = SubStatus.Overdue},
                    new SubscriptionItems(){Id = 6, OwnerId = 2, Service = "Amazon", Status = SubStatus.Active},

                    new SubscriptionItems(){Id = 7, OwnerId = 3, Service = "YouTube",Status = SubStatus.Archived},
                    new SubscriptionItems(){Id = 8, OwnerId = 3, Service = "Netflix", Status = SubStatus.Active},

                    new SubscriptionItems(){Id = 9, OwnerId = 4, Service = "YouTube", Status = SubStatus.Active},
                    new SubscriptionItems(){Id = 10, OwnerId = 4, Service = "Netflix", Status = SubStatus.Active},
                    new SubscriptionItems(){Id = 11, OwnerId = 4, Service = "Megogo", Status = SubStatus.Archived},
                    new SubscriptionItems(){Id = 12, OwnerId = 4, Service = "AppleFilms", Status = SubStatus.Active},

                    new SubscriptionItems(){Id = 13, OwnerId = 5, Service = "AppleSerice", Status = SubStatus.Active},
                    new SubscriptionItems(){Id = 14, OwnerId = 5, Service = "Xbox", Status = SubStatus.Active},
                    new SubscriptionItems(){Id = 15, OwnerId = 5, Service = "AppleFilms", Status = SubStatus.Active},

                };
    }
}
