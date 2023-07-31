using System.Net;

namespace WebApp.ApiTests
{
  [Collection("Test collection")]
  public class AddressControllerTests : IAsyncLifetime
  {
    private readonly HttpClient _httpClient;
    private readonly Func<Task> _resetDatabase;

    public AddressControllerTests(CustomWebApplicationFactory factory) 
    {
      _httpClient = factory.HttpClient;
      _resetDatabase = factory.ResetDatabaseAsync;
    }

    [Fact]
    public async Task PostTestAsync()
    {
      //Arrange

      //Act
      HttpResponseMessage addMessage = await _httpClient.PostAsync("address/Philipp/holl", null);
      Assert.True(addMessage.StatusCode == HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostTest1Async()
    {
      //Arrange

      //Act
      HttpResponseMessage addMessage = await _httpClient.PostAsync("address/Philipp/holl", null);
      Assert.True(addMessage.StatusCode == HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostTest2Async()
    {
      //Arrange

      //Act
      HttpResponseMessage addMessage = await _httpClient.PostAsync("address/Philipp/holl", null);
      Assert.True(addMessage.StatusCode == HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostTest3Async()
    {
      //Arrange

      //Act
      HttpResponseMessage addMessage = await _httpClient.PostAsync("address/Philipp/holl", null);
      Assert.True(addMessage.StatusCode == HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostTest4Async()
    {
      //Arrange

      //Act
      HttpResponseMessage addMessage = await _httpClient.PostAsync("address/Philipp/holl", null);
      Assert.True(addMessage.StatusCode == HttpStatusCode.OK);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();
  }
}
