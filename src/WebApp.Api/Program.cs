using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApp.Common.Data;
using WebApp.Common.Interfaces;
using WebApp.Common.Services;

namespace WebApp.Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      builder.Services.AddDbContext<AppDataContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("DataConnection")));

      builder.Services.AddScoped<IDataAccessService, DataAccessService>();

      builder.Services.AddControllers().AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        //options.JsonSerializerOptions.DefaultIgnoreCondition =
        //       JsonIgnoreCondition.WhenWritingNull;
      });
      ;
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      WebApplication app = builder.Build();
      DoAfterBuildAction(app);

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }

    private static void DoAfterBuildAction(WebApplication app)
    {
      using (IServiceScope serviceScope = app.Services.CreateScope())
      {
        IServiceProvider serviceProvider = serviceScope.ServiceProvider;

        IDataAccessService? dataAccessService = serviceProvider.GetRequiredService<IDataAccessService>();
        if (dataAccessService != null)
        {
          dataAccessService.MigrateDatabaseAsync().GetAwaiter().GetResult();
        }
      }
    }
  }
}
