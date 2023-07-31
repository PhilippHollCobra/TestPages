using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Common.Data;
using WebApp.Common.Interfaces;
using WebApp.Common.Models;

namespace WebApp.Common.Services
{
  public class DataAccessService : IDataAccessService
  {
    private readonly ILogger<DataAccessService> _logger;
    private readonly AppDataContext _context;

    public DataAccessService(ILogger<DataAccessService> logger, AppDataContext context)
    {
      _logger = logger;
      _context = context;
    }

    public async Task<bool> MigrateDatabaseAsync()
    {
      try
      {
        await _context.Database.MigrateAsync();
        return true;
      }
      catch (Exception ex)
      {
        _logger.LogCritical("Migrate", ex);
        return false;
      }
    }

    public async Task<bool> CreateDefaultDataAsync()
    {
      try
      {
        if (!_context.Addresses.Any())
        {
          await _context.Addresses.AddAsync(new Address
          {
            Id = Guid.Parse("df88dc55-b94f-4ba1-a132-fb81498e9995"),
            FirstName = "System",
            LastName = "System"
          });
        }

        if (!_context.MyTasks.Any())
        {
          await _context.MyTasks.AddRangeAsync(new List<MyTask>
          {
            new MyTask
            {
              Id = Guid.Parse("205e0754-b042-4888-9dd2-c6467c6e76fd"),
              Name = "Default1",
              TaskType = TaskType.Default,
            },
            new MyTask
            {
              Id = Guid.Parse("45ef7084-4784-4a0c-80f7-90b93aaa1e38"),
              Name = "Default2",
              TaskType = TaskType.Default,
            },

          });
        }

        await _context.SaveChangesAsync();
        return true;
      }
      catch (Exception ex)
      {
        _logger.LogCritical("Migrate", ex);
        return false;
      }
    }

    public async Task<T?> CreateObjectAsync<T>(T item) where T : Entity
    {
      try
      {
        await _context.Set<T>().AddAsync(item);
        await _context.SaveChangesAsync();

        return item;
      }
      catch (Exception ex)
      {
        _logger.LogCritical("CreateObject", ex);
        return null;
      }
    }

    public async Task<bool> DeleteObjectAsync<T>(Guid id) where T : Entity
    {
      try
      {
        T? item = await _context.Set<T>().FindAsync(id);
        if (item != null)
        {
          _context.Set<T>().Remove(item);
          await _context.SaveChangesAsync();
          return true;
        }

        return false;
      }
      catch (Exception ex)
      {
        _logger.LogCritical("CreateObject", ex);
        return false;
      }
    }

    public async Task<T?> GetObjectAsync<T>(Guid id) where T : Entity
    {
      try
      {
        T? item = await _context.Set<T>().FindAsync(id);
        return item;
      }
      catch (Exception ex)
      {
        _logger.LogCritical("GetObject", ex);
        return null;
      }
    }

    public async Task<T?> UpdateObjectAsync<T>(T item) where T : Entity
    {
      try
      {
        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync();

        return item;
      }
      catch (Exception ex)
      {
        _logger.LogCritical("GetObject", ex);
        return null;
      }
    }

    public async Task<List<T>?> GetObjectsAsync<T>() where T : Entity
    {
      try
      {
        List<T> items = await _context.Set<T>().ToListAsync();
        return items;
      }
      catch (Exception ex)
      {
        _logger.LogCritical("GetObject", ex);
        return null;
      }
    }
  }
}
