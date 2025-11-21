using IntegrationTest.Setting;
using LW4_task_3.Models.Request;
using LW4_task_3.Models.Response;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Serializers;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace IntegrationTest
{
    public class PeopleController : BaseIntegation
    {
        public PeopleController(CustomWebApplicationFactory factory) : base(factory) {}



        [Fact]
        public async Task GetAllPeople_ReturnSeedItems()
        {
            var response = await httpClient.GetAsync("/People");    

            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<List<PeopleResponse>>();

            Assert.NotNull(items);
            Assert.Equal(5, items.Count);
            Assert.Contains(items, item => item.Name == "Kate");
        }

        [Fact]
        public async Task GetById_ReturnPeople()
        {
            var peoples = await httpClient.GetFromJsonAsync<List<PeopleResponse>>("/People");
            string id = peoples[0].Id;

            var responce = await httpClient.GetAsync($"/People/{id}");

            responce.EnsureSuccessStatusCode();

            var responsePeople = await responce.Content.ReadFromJsonAsync<PeopleResponse>();

            Assert.NotNull(responsePeople);
            Assert.Equal(id, responsePeople.Id);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnNotFound()
        {
            var response = await httpClient.GetAsync("/People/11111");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsCreatedItem()
        {
            var newPeople = new PeopleRequest
            {
                Name = "Newpeople",
                Email = "NewEmail@gmail.com"
            };

            var response = await httpClient.PostAsJsonAsync("/People", newPeople);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var people = await response.Content.ReadFromJsonAsync<PeopleResponse>();

            Assert.NotNull(people);
            Assert.Equal(newPeople.Name, people.Name);
            Assert.Equal(newPeople.Email, people.Email);
            Assert.False(string.IsNullOrEmpty(people.Id));

            var list = await httpClient.GetFromJsonAsync<List<PeopleResponse>>("/People");
            Assert.Equal(list.Count, 6);
        }

        [Fact]
        public async Task Update_ReturnNoContent_UpdateDB()
        {
            var peoples = await httpClient.GetFromJsonAsync<List<PeopleResponse>>("/People");
            string id = peoples[0].Id;

            var updatedPeople = new PeopleRequest
            {
                Name = "Updatename",
                Email = "UpdateEmail@gmail.com"
            };

            var responce = await httpClient.PutAsJsonAsync($"/People/{id}",updatedPeople);

            Assert.Equal(HttpStatusCode.NoContent, responce.StatusCode);

            var p = await httpClient.GetFromJsonAsync<PeopleResponse>($"/People/{id}");

            Assert.Equal(updatedPeople.Name,p.Name);
            Assert.Equal(updatedPeople.Email, p.Email);

        }

        [Fact]
        public async Task Update_InvalidId_ReturnNotFound()
        {
            var response = await httpClient.PutAsJsonAsync(
                "/People/68f5e054f21b02f6aece46e5",
                new PeopleRequest() { Name = "Somename", Email = "SomeEmail@gmail.com" 
             });

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePart_ReturnNoContent_UpdateDB()
        {
            var peoples = await httpClient.GetFromJsonAsync<List<PeopleResponse>>("/People");
            string id = peoples[0].Id;

            
            string jsonPatch = @"{""name"": ""Patchname""}";
            var doc = JsonDocument.Parse(jsonPatch);
            var element = doc.RootElement;

            var responce = await httpClient.PatchAsJsonAsync($"/People/{id}", element);

            Assert.Equal(HttpStatusCode.NoContent, responce.StatusCode);

            var pathedPeople = await httpClient.GetFromJsonAsync<PeopleResponse>($"/People/{id}");

            Assert.Equal(peoples[0].Email, pathedPeople.Email);
            Assert.Equal("Patchname", pathedPeople.Name);

        }

        [Fact]
        public async Task UpdatePart_InvalidId_ReturnNotFound()
        {
            string jsonPatch = @"{""name"": ""Patchname""}";
            var doc = JsonDocument.Parse(jsonPatch);
            var element = doc.RootElement;

            var response = await httpClient.PatchAsJsonAsync(
                "/People/68f5e054f21b02f6aece46e5",
                element
            );

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeletePeople_ReturnNoContent_CheckDB()
        {
            var list = await httpClient.GetFromJsonAsync<List<PeopleResponse>>("/People");

            string id = list[0].Id;

            var resoponce = await httpClient.DeleteAsync($"/People/{id}");

            Assert.Equal(HttpStatusCode.NoContent, resoponce.StatusCode);

            list = await httpClient.GetFromJsonAsync<List<PeopleResponse>>("/People");

            Assert.Equal(4, list.Count);

        }

        [Fact]
        public async Task Delete_InvalidId_NotFound()
        {
            var responce = await httpClient.DeleteAsync("/People/68f5e054f21b02f6aece46e5");

            Assert.Equal(HttpStatusCode.NotFound, responce.StatusCode);
        }

    }
}