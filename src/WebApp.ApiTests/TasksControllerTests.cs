using System.Net;
using System.Text.Json;
using WebApp.Common.Models;

namespace WebApp.ApiTests
{
  [Collection("Test collection")]
  public class TasksControllerTests : IAsyncLifetime
  {
    private readonly HttpClient _httpClient;
    private readonly Func<Task> _resetDatabase;

    public TasksControllerTests(CustomWebApplicationFactory factory)
    {
      _httpClient = factory.HttpClient;
      _resetDatabase = factory.ResetDatabaseAsync;
    }

    [Fact]
    public async Task GetTestAsync()
    {
      //Arrange

      //Act
      HttpResponseMessage responseMessage = await _httpClient.GetAsync("tasks/205e0754-b042-4888-9dd2-c6467c6e76fd");
      string httpContent = await responseMessage.Content.ReadAsStringAsync();
      MyTask? task = JsonSerializer.Deserialize<MyTask>(httpContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

      //Assert
      Assert.True(responseMessage.StatusCode == HttpStatusCode.OK);
      Assert.NotNull(task);
    }


    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();
  }
}
