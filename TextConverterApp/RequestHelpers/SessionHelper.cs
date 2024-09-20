namespace TextConverterApp.RequestHelpers;

public class SessionHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetSessionId()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            return "nobody";
        }

        var sessionId = context.Session.GetString("sessionId");
        if (string.IsNullOrEmpty(sessionId))
        {
            sessionId = Guid.NewGuid().ToString();
            context.Session.SetString("sessionId", sessionId);
        }

        return sessionId;
    }
}