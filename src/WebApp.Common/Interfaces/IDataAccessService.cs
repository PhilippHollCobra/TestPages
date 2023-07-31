using WebApp.Common.Models;

namespace WebApp.Common.Interfaces
{
  public interface IDataAccessService
  {
    Task<bool> MigrateDatabaseAsync();
    Task<bool> CreateDefaultDataAsync();
    Task<T?> GetObjectAsync<T>(Guid id) where T : Entity;
    Task<List<T>?> GetObjectsAsync<T>() where T : Entity;
    Task<T?> CreateObjectAsync<T>(T item) where T : Entity;
    Task<T?> UpdateObjectAsync<T>(T item) where T : Entity;
    Task<bool> DeleteObjectAsync<T>(Guid id) where T : Entity;
  }
}
