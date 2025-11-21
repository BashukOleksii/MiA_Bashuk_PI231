using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Setting
{
    public class BaseIntegation: IClassFixture<CustomWebApplicationFactory>
    {
        protected readonly HttpClient httpClient;
        protected readonly IServiceProvider serviceProvider;

        public BaseIntegation(CustomWebApplicationFactory factory)
        {
            httpClient = factory.CreateClient();
            serviceProvider = factory.Services;

            RunSeedAsync().Wait();
        }

        private async Task RunSeedAsync()
        {
            using var scope = serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<SeedPeople>();
            await seeder.SeedPeoplesAsync();
        }
    }
}
