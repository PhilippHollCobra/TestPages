using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ApiTests
{
  public class WeatherForecastControllerTests
  {
    private readonly HttpClient _httpClient = new () { BaseAddress = new Uri("https://localhost:7267") };

    [Fact]
    public async void GetTestAsync()
    {
      //Arrange

      //Act
      HttpResponseMessage responseMessage = await _httpClient.GetAsync("weatherforecast/GetWeatherForecast");

      //Assert
      Assert.True(responseMessage.StatusCode == HttpStatusCode.OK);

    }
  }
}
