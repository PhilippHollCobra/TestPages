using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace WebApp.Api.Logging
{
  public class CustomLoggerProvider : ILoggerProvider
  {
    internal const string COBRA_REQUESTID_HEADERNAME = "x-cobra-requestid";
    ConsoleLoggerProvider ConsoleLoggerProvider { get; }

    public void Dispose()
    {
      ConsoleLoggerProvider.Dispose();
    }

    public CustomLoggerProvider(IOptionsMonitor<ConsoleLoggerOptions> options)
    {
      options.CurrentValue.DisableColors = true;
      ConsoleLoggerProvider = new ConsoleLoggerProvider(options);
    }
    public ILogger CreateLogger(string categoryName)
    {
      return new CustomConsoleLogger(categoryName, ConsoleLoggerProvider.CreateLogger(categoryName));
    }

    public class CustomConsoleLogger : ILogger
    {
      private readonly ILogger _consoleLogger;

      public CustomConsoleLogger(string categoryName, ILogger consoleLogger)
      {
        _consoleLogger = consoleLogger;
      }

      public IDisposable BeginScope<TState>(TState state)
      {
        return _consoleLogger.BeginScope(state);
      }

      public bool IsEnabled(LogLevel logLevel)
      {
        return _consoleLogger.IsEnabled(logLevel);
      }

      public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
      {
        if (!IsEnabled(logLevel))
          return;

        string headers = string.Empty;

        string? operationId = Activity.Current?.GetBaggageItem(COBRA_REQUESTID_HEADERNAME);

        if (Guid.TryParse(operationId, out Guid requestIdGuid))
          headers = $" ({COBRA_REQUESTID_HEADERNAME}:{requestIdGuid})";


        _consoleLogger.Log(logLevel, eventId, exception, $"{formatter(state, exception)}{headers}");
      }
    }
  }
}
