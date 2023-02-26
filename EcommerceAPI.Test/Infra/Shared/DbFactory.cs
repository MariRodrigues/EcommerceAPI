using EcommerceAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EcommerceAPI.Test.Infra.Shared
{
    public static class DbFactory
    {
        public static AppDbContext CreateAppDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySQL("server=localhost;database=ecommerceDB;user=root;password=root").Options;

            return new AppDbContext(optionsBuilder);
        }
    }
}
