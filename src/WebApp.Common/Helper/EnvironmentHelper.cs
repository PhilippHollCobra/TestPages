namespace WebApp.Common.Helper
{
  public enum EnvironmentType
  {
    Unkown,
    Production,
    Development,
    Staging,
    Testing
  }

  public static class EnvironmentHelper
  {
    private static EnvironmentType? _environment;
    public static EnvironmentType Environment
    {
      get
      {
        if (_environment == null)
        {
          string? environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
          _environment = environment switch
          {
            "Production" => EnvironmentType.Production,
            "Development" => EnvironmentType.Development,
            "Staging" => EnvironmentType.Staging,
            "Apitests" => EnvironmentType.Testing,
            _ => EnvironmentType.Unkown,
          };
        }

        return _environment.Value;
      }
    }
    
    public static bool IsDevelopmentOrTesting() => Environment == EnvironmentType.Development || Environment == EnvironmentType.Testing;
    public static bool IsDevelopment() => Environment == EnvironmentType.Development;
    public static bool IsTesting() => Environment == EnvironmentType.Testing;
    public static bool IsStaging() => Environment == EnvironmentType.Staging;
    public static bool IsProduction() => Environment == EnvironmentType.Production;
    public static bool IsUnkown() => Environment == EnvironmentType.Unkown;
  }
}
