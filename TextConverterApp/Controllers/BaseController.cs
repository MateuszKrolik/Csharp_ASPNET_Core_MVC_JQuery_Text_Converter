using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TextConverterApp.RequestHelpers;

namespace TextConverterApp.Controllers;

public class BaseController : Controller
{
    private readonly SessionHelper _sessionHelper;

    public BaseController(SessionHelper sessionHelper)
    {
        _sessionHelper = sessionHelper;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var sessionId = _sessionHelper.GetSessionId();
        ViewBag.SessionId = sessionId;
        base.OnActionExecuting(context);
    }
}
