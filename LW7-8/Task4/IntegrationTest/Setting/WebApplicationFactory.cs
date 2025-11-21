using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mongo2Go;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace IntegrationTest.Setting
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private MongoDbRunner runner;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(service =>
            {
                runner = MongoDbRunner.Start();

                var descriptor = service.SingleOrDefault(s => s.ServiceType == typeof(IMongoClient));

                if (descriptor is not null)
                    service.Remove(descriptor);

                service.AddSingleton<IMongoClient>(sp => new MongoClient(runner.ConnectionString));

                service.AddSingleton(sp =>
                {
                    var Client = sp.GetRequiredService<IMongoClient>();
                    return Client.GetDatabase("IntegrateDB");
                });

                service.AddSingleton<SeedPeople>();

            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            runner?.Dispose();
        }
    }
}
