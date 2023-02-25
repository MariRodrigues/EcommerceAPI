using EcommerceAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EcommerceAPI.Test.Infra.Shared
{
    public static class DbFactory
    {
        private static IConfiguration GetConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.test.json");
            configurationBuilder.AddJsonFile(configPath);

            return configurationBuilder.Build();
        }

        public static AppDbContext CreateAppDbContext()
        {
            var configuration = GetConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySQL(configuration.GetConnectionString("DefaultConnection")).Options;

            return new AppDbContext(optionsBuilder);
        }
    }
}
