using LW4_task_3;
using LW4_task_3.Interface.InterfacesRepository;
using LW4_task_3.Models.Entities;
using LW4_task_3.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System.Xml.Linq;
using ZstdSharp.Unsafe;

namespace SubServiceTest
{
    public class SubServiceTest
    {
        private readonly Mock<ISubRepository> _subRepositoryMock;
        private readonly Mock<IPeopleRepository> _peopleRepository;
        private readonly SubService _subService;

        private List<SubscriptionItem> GetSubscriptions()
        {
            return new List<SubscriptionItem>
            {
                    new SubscriptionItem() { Id = "1", OwnerId = "1", Service = "Netflix", Status = SubStatus.Active },
                    new SubscriptionItem() { Id = "2", OwnerId = "1", Service = "Xbox", Status = SubStatus.Active },
                    new SubscriptionItem() { Id = "3", OwnerId = "1", Service = "Amazon", Status = SubStatus.Active },

                    new SubscriptionItem() { Id = "4", OwnerId = "2", Service = "Steam", Status = SubStatus.Active },
                    new SubscriptionItem() { Id = "5", OwnerId = "2", Service = "Google", Status = SubStatus.Overdue },
                    new SubscriptionItem() { Id = "6", OwnerId = "2", Service = "Google", Status = SubStatus.Active },

                    new SubscriptionItem() { Id = "7", OwnerId = "3", Service = "Google", Status = SubStatus.Archived },
                    new SubscriptionItem() { Id = "8", OwnerId = "3", Service = "Netflix", Status = SubStatus.Active },

                    new SubscriptionItem() { Id = "9", OwnerId = "4", Service = "Google", Status = SubStatus.Active },
                    new SubscriptionItem() { Id = "10", OwnerId = "4", Service = "Netflix", Status = SubStatus.Active },
                    new SubscriptionItem() { Id = "11", OwnerId = "4", Service = "Megogo", Status = SubStatus.Archived },
                    new SubscriptionItem() { Id = "12", OwnerId = "4", Service = "Amazon", Status = SubStatus.Active },

                    new SubscriptionItem() { Id = "13", OwnerId = "5", Service = "AppleSerice", Status = SubStatus.Active },
                    new SubscriptionItem() { Id = "14", OwnerId = "5", Service = "Xbox", Status = SubStatus.Active },
                    new SubscriptionItem() { Id = "15", OwnerId = "5", Service = "Amazon", Status = SubStatus.Active }
            };
        }

        public SubServiceTest()
        {
            _subRepositoryMock = new Mock<ISubRepository>();
            _peopleRepository = new Mock<IPeopleRepository>();
            _subService = new SubService(_subRepositoryMock.Object, _peopleRepository.Object);
        }


        [Fact]
        public async Task CountAllSub_RuturnCount()
        {
            var elements = GetSubscriptions();
            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(null, null, null)).ReturnsAsync(elements);

            int count = await _subService.CountAllSub();

            Assert.Equal(elements.Count, count);
        }

        [Fact]
        public async Task CountAllSub_Return0()
        {
            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(null, null, null)).ReturnsAsync(Enumerable.Empty<SubscriptionItem>);

            int count = await _subService.CountAllSub();

            Assert.Equal(0, count);
        }






        [Theory]
        [InlineData("Xbox", ((double)2 / 15) * 100, 0.001)]
        [InlineData("Netflix", ((double)3 / 15) * 100, 0.001)]
        [InlineData("Anonim", 0, 0.001)]
        public async Task PercentService_WhenColectionIsNotEmpty(string name, double percent, double normal)
        {
            var elements = GetSubscriptions();

            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(null, null, null)).ReturnsAsync(elements);

            double percentService = await _subService.PercentService(name);

            Assert.Equal(percent, percentService, normal);
        }


        [Fact]
        public async Task PercentService_WhenColectionIsEmpty()
        {
            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(null, null, null)).ReturnsAsync(Enumerable.Empty<SubscriptionItem>);

            double percent = await _subService.PercentService("Netflix");

            Assert.Equal(0, percent);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task PercentService_ArgumentNullExceprion(string? name)
        {

            await Assert.ThrowsAsync<ArgumentNullException>(() => _subService.PercentService(name));
        }







        [Fact]
        public async Task PopularStatus_NotEmpty()
        {
            var element = GetSubscriptions();

            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(null, null, null)).ReturnsAsync(element);

            var status = await _subService.PopularStatus();

            Assert.Equal(SubStatus.Active, status);
        }

        [Fact]
        public async Task PopularStatus_CollectionEmpty()
        {
            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(null, null, null)).ReturnsAsync(Enumerable.Empty<SubscriptionItem>);

            var status = await _subService.PopularStatus();

            Assert.Equal(SubStatus.None, status);
        }





        [Theory]
        [InlineData(SubStatus.Active, 12)]
        [InlineData(SubStatus.Overdue, 1)]
        [InlineData(SubStatus.Archived, 2)]
        public async Task CountSubByStatus_TestReturn(SubStatus subStatus, int count)
        {
            var elements = GetSubscriptions();

            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(null, null, subStatus)).
                ReturnsAsync(elements.Where(s => s.Status == subStatus));

            int c = await _subService.CountSubByStatus(subStatus);

            Assert.Equal(count, c);
        }

        [Theory]
        [InlineData(SubStatus.None)]
        [InlineData(null)]
        public async Task CountSubByStatus_InvalidStatus(SubStatus subStatus)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _subService.CountSubByStatus(subStatus));
        }

        [Fact]
        public async Task CountSubByStatus_EmptyCollection()
        {
            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(null, null, SubStatus.Active)).ReturnsAsync(Enumerable.Empty<SubscriptionItem>);

            int count = await _subService.CountSubByStatus(SubStatus.Active);

            Assert.Equal(0, count);
        }








        [Fact]
        public async Task Top3Service_NotEmptyCollection()
        {
            var elements = GetSubscriptions();

            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(null, null, null)).ReturnsAsync(elements);

            var services = await _subService.Top3Service();

            Assert.Equal(services, new string[] { "Google", "Netflix", "Amazon" });

        }

        [Fact]
        public async Task Top3Service_EmptyCollection()
        {
            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(null, null, null)).ReturnsAsync(Enumerable.Empty<SubscriptionItem>);

            var services = await _subService.Top3Service();

            Assert.Empty(services);

        }






        [Fact]
        public async Task Create_ValidOwner()
        {
            var sub = new SubscriptionItem { Id = "New", OwnerId = "Valid", Service = "Netflix", Status = SubStatus.Active };

            _peopleRepository.Setup(r => r.IsExist(sub.OwnerId)).ReturnsAsync(true);

            await _subService.CreateAsync(sub);

            _subRepositoryMock.Verify(r => r.CreateAsync(sub), Times.Once);

            _peopleRepository.Verify(r => r.IsExist(sub.OwnerId), Times.Once);
        }

        [Fact]
        public async Task Create_InvalidOwner_ArgumentException()
        {
            var sub = new SubscriptionItem { Id = "New", OwnerId = "Invalid", Service = "Netflix", Status = SubStatus.Active };

            _peopleRepository.Setup(r => r.IsExist(sub.OwnerId)).ReturnsAsync(false);

            await Assert.ThrowsAsync<ArgumentException>(() => _subService.CreateAsync(sub));

            _subRepositoryMock.Verify(r => r.CreateAsync(sub), Times.Never);
            _peopleRepository.Verify(r => r.IsExist(sub.OwnerId), Times.Once);

        }

        //_peopleRepository.Setup(r => r.IsExist(sub.Id)).ReturnsAsync(true);





        [Fact]
        public async Task GetById_Exist()
        {
            var sub = GetSubscriptions()[1];

            _subRepositoryMock.Setup(r => r.GetByIdAsync(sub.Id)).ReturnsAsync(sub);

            var subFrom = await _subService.GetByIdAsync(sub.Id);

            Assert.Equal(sub, subFrom);

            _subRepositoryMock.Verify(r => r.GetByIdAsync(sub.Id), Times.Once);
        }

        [Fact]
        public async Task GetById_NotFound_KeyNorFoundException()
        {
            string id = "SomeId";
            _subRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((SubscriptionItem)null!);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _subService.GetByIdAsync(id));

        }


        public static IEnumerable<object[]> GetSubs() =>
            new List<object[]>
            {
                new object[] {null,null,null },
                new object[] { "1",null,null},
                new object[] {null,"Netflix",null},
                new object[] { null,null,SubStatus.Active},
                new object[] { "1", null, SubStatus.Active},
                new object[] {null, "Netflix", SubStatus.Active},
                new object[] { "1", "Netflix", SubStatus.Active},
                new object[] { "1", "Netflix", SubStatus.Active}
            };
        [Theory]
        [MemberData(nameof(GetSubs))]
        public async Task GetSubscriptionsItems_ValidDate(string? ownerId, string? service, SubStatus? subStatus)
        {
            var elements = GetSubscriptions();

            if (ownerId is not null)
                elements = elements.Where(x => x.Id == ownerId).ToList();
            if (service is not null)
                elements = elements.Where(x => x.Service == service).ToList();
            if (subStatus is not null)
                elements = elements.Where(x => x.Status == subStatus).ToList();

            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(ownerId, service, subStatus)).ReturnsAsync(elements);

            var subs = await _subService.GetSubscriptionsItemsAsync(ownerId, service, subStatus);

            Assert.Equal(elements, subs);

        }

        [Theory]
        [MemberData(nameof(GetSubs))]
        public async Task GetSubscriptionsItems_NotFound(string? ownerId, string? service, SubStatus? subStatus)
        {
            _subRepositoryMock.Setup(r => r.GetSubscriptionsItemsAsync(ownerId, service, subStatus)).ReturnsAsync(Enumerable.Empty<SubscriptionItem>);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _subService.GetSubscriptionsItemsAsync(ownerId, service, subStatus));
        }






        [Fact]
        public async Task Update_ValidOwnerAndExistSub()
        {
            string id = "UpdateId";
            var sub = new SubscriptionItem { Id = id, OwnerId = "ValidOwner", Service = "Service", Status = SubStatus.Active };

            _subRepositoryMock.Setup(r => r.IsExist(id)).ReturnsAsync(true);
            _peopleRepository.Setup(r => r.IsExist(sub.OwnerId)).ReturnsAsync(true);

            await _subService.UpdateAsync(id, sub);

            _subRepositoryMock.Verify(r => r.IsExist(id), Times.Once);
            _subRepositoryMock.Verify(r => r.UpdateAsync(id, sub), Times.Once);
            _peopleRepository.Verify(r => r.IsExist(sub.OwnerId), Times.Once);



        }

        [Fact]
        public async Task Update_NotFoundSub()
        {
            string id = "Invalid";

            _subRepositoryMock.Setup(r => r.IsExist(id)).ReturnsAsync(false);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _subService.UpdateAsync(id, new SubscriptionItem()));
        }

        [Fact]
        public async Task Update_InvalidOwner()
        {
            string id = "Valid";
            var sub = new SubscriptionItem() { OwnerId = "Invalid" };

            _subRepositoryMock.Setup(r => r.IsExist(id)).ReturnsAsync(true);
            _peopleRepository.Setup(r => r.IsExist(sub.Id)).ReturnsAsync(false);

            await Assert.ThrowsAsync<ArgumentException>(() => _subService.UpdateAsync(id, sub));
        }




        [Fact]
        public async Task Delete_IdExit()
        {
            string id = "Valid";

            _subRepositoryMock.Setup(r => r.IsExist(id)).ReturnsAsync(true);

            await _subService.DeleteAsync(id);

            _subRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task Delete_IdDontExist()
        {
            string id = "Invalid";

            _subRepositoryMock.Setup(r => r.IsExist(id)).ReturnsAsync(false);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _subService.DeleteAsync(id));
        } 





    }
}