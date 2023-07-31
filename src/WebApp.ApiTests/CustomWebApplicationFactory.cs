using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Respawn;
using System.Data.Common;
using WebApp.Api;
using WebApp.Common.Interfaces;

namespace WebApp.ApiTests
{
  public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
  {
    public HttpClient HttpClient { get; private set; } = default!;
    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;


    protected override IHost CreateHost(IHostBuilder builder)
    {
      builder.UseEnvironment("Apitests");
      return base.CreateHost(builder);
    }

    public async Task InitializeAsync()
    {
      HttpClient = CreateClient();

      using (IServiceScope scope = Services.CreateScope())
      {
        IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        _dbConnection = new SqlConnection(configuration.GetConnectionString("DataConnection"));
      }

      await _dbConnection.OpenAsync();
      _respawner = await Respawner.CreateAsync(_dbConnection);
    }

    Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;

    public async Task ResetDatabaseAsync() => await _respawner.ResetAsync(_dbConnection);
  }
}
