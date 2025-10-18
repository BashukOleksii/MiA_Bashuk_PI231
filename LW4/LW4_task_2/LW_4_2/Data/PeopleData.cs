using LW_4_2.Models;

namespace LW_4_2.Data
{
    public static class PeopleData
    {
        public static List<PeopleItems> peopleItems = new List<PeopleItems>()
        {
                    new PeopleItems() {Id = 1, Name = "Kate", Email = "Kate@Email.com" },
                    new PeopleItems() {Id = 2, Name = "John", Email = "John@Email.com" },
                    new PeopleItems() {Id = 3, Name = "Jane", Email = "Jane@Email.com" },
                    new PeopleItems() {Id = 4, Name = "Steve", Email = "Steve@Email.com" },
                    new PeopleItems() {Id = 5, Name = "Alex", Email = "Alex@Email.com" }
        };
    }
}
