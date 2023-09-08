using System.Diagnostics;

namespace WebApp.Api.Logging
{
  public class RequestIdMiddleware
  {
    private RequestDelegate _next;

    public RequestIdMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context, ILogger<RequestIdMiddleware> logger)
    {
      if (!Guid.TryParse(context.Request.Headers[CustomLoggerProvider.COBRA_REQUESTID_HEADERNAME], out Guid requestId))
        requestId = Guid.NewGuid();

      Activity.Current.AddBaggage(CustomLoggerProvider.COBRA_REQUESTID_HEADERNAME, requestId.ToString());
      context.Response.Headers[CustomLoggerProvider.COBRA_REQUESTID_HEADERNAME] = requestId.ToString();

      await _next(context);
    }
  }
}
