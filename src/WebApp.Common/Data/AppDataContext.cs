using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApp.Common.Models;

namespace WebApp.Common.Data
{
  public class AppDataContext : DbContext
  {
    public DbSet<Address> Addresses { get; set; }
    public DbSet<MyTask> MyTasks { get; set; }

    public AppDataContext() { }

    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory() + "/../WebApp.Api")
               .AddJsonFile("appsettings.json")
               .Build();
        string? connectionString = configuration.GetConnectionString("DataConnection");
        optionsBuilder.UseSqlServer(connectionString);
      }
    }
  }
}
