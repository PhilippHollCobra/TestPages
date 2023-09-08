using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace WebApp.Api.Logging
{
  public static class LogExtension
  {
    public static ILoggingBuilder AddCustomLoggerProvider(this ILoggingBuilder builder)
    {
      builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());
      return builder;
    }

    public static Guid GetCobraTrackingId(this Activity activity)
    {
      string requestId = activity?.GetBaggageItem(CustomLoggerProvider.COBRA_REQUESTID_HEADERNAME);
      if (!Guid.TryParse(requestId, out Guid requestIdGuid))
        requestIdGuid = Guid.NewGuid();

      return requestIdGuid;
    }

    public static void AddCobraTrackingIdHeader(this HttpRequestHeaders headers) =>
      headers.Add(CustomLoggerProvider.COBRA_REQUESTID_HEADERNAME, Activity.Current.GetCobraTrackingId().ToString());
  }
}
