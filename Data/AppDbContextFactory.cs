using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CityDriveManager.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // Fallback hardcoded server version for design-time CLI migrations generation without connecting to MySQL server
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
            optionsBuilder.UseMySql("Server=localhost;Database=citydrive;User=root;Password=;", serverVersion);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
