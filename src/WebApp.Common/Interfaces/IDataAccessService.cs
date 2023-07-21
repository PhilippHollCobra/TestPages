using WebApp.Common.Models;

namespace WebApp.Common.Interfaces
{
  public interface IDataAccessService
  {
    Task<bool> MigrateDatabaseAsync();
    Task<T?> GetObjectAsync<T>(int id) where T : Entity;
    Task<List<T>?> GetObjectsAsync<T>() where T : Entity;
    Task<T?> CreateObjectAsync<T>(T item) where T : Entity;
    Task<T?> UpdateObjectAsync<T>(T item) where T : Entity;
    Task<bool> DeleteObjectAsync<T>(int id) where T : Entity;
  }
}
