using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Common.Models;
using System.Net.Http.Json;

namespace WebApp.ApiTests
{
  public class AddressControllerTests
  {
    private readonly HttpClient _httpClient = new () { BaseAddress = new Uri("http://localhost:9001") };

    [Fact]
    public async void GetTestAsync()
    {
      //Arrange

      //Act
      HttpResponseMessage addMessage = await _httpClient.PostAsync("address/Philipp/holl", null);
      Assert.True(addMessage.StatusCode == HttpStatusCode.OK);


      HttpResponseMessage responseMessage = await _httpClient.GetAsync("address/1");
      string httpContent = await responseMessage.Content.ReadAsStringAsync();
      Address? items = JsonSerializer.Deserialize<Address>(httpContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

      //Assert
      Assert.True(responseMessage.StatusCode == HttpStatusCode.OK);
      Assert.NotNull(items);
      Assert.True(items.Id == 1);

    }
  }
}
