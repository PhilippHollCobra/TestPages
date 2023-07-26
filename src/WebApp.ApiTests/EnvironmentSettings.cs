namespace WebApp.ApiTests
{
  public static class EnvironmentSettings
  {
    public static string ApiBaseUrl => Environment.GetEnvironmentVariable("API_BASE_URL") ?? "http://localhost:9001";
  }
}
