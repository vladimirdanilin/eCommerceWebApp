using ECommerceWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerceWebApp.Data
{
    public class DatabaseSeedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseSeedService(IServiceProvider serviceProvider)
        { 
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                await AppDbInitializer.SeedAsync(scope.ServiceProvider, userManager, roleManager);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

    }
}
