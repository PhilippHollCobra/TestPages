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

    public async Task<bool> DeleteObjectAsync<T>(int id) where T : Entity
    {
      try
      {
        T? item = await _context.Set<T>().FindAsync(id);
        if(item != null)
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

    public async Task<T?> GetObjectAsync<T>(int id) where T : Entity
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
