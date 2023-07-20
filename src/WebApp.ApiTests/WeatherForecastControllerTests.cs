using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApp.ApiTests
{
  public class WeatherForecastControllerTests
  {
    private readonly HttpClient _httpClient = new () { BaseAddress = new Uri("http://localhost:9001") };

    [Fact]
    public async void GetTestAsync()
    {
      //Arrange

      //Act
      HttpResponseMessage responseMessage = await _httpClient.GetAsync("weatherforecast");
      string httpContent = await responseMessage.Content.ReadAsStringAsync();
      IEnumerable<WeatherForecast>? items = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(httpContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

      //Assert
      Assert.True(responseMessage.StatusCode == HttpStatusCode.OK);
      Assert.NotNull(items);
      Assert.True(items.Any());

    }
  }

  public class WeatherForecast
  {
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);

    public string? Summary { get; set; }
  }
}
