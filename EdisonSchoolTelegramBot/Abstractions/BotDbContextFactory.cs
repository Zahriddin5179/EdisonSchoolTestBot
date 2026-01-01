using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EdisonSchoolTelegramBot.Abstractions
{
    public class BotDbContextFactory : IDesignTimeDbContextFactory<BotDbContext>
    {
        public BotDbContext CreateDbContext(string[] args)
        {
            // Loyiha root'ini aniq belgilash
            var basePath = Directory.GetCurrentDirectory();

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            // Agar Abstractions alohida papkada bo'lsa, quyidagicha ham qilish mumkin:
            // var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BotDbContext>();
            var connectionString = configuration.GetConnectionString("Default");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("ConnectionString 'Default' topilmadi!");

            optionsBuilder.UseNpgsql(connectionString);

            return new BotDbContext(optionsBuilder.Options);
        }
    }
}
