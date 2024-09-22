using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using TextConverterApp.Models;
using TextConverterApp.RequestHelpers;
using TextConverterApp.Services.TextConverterService;
using TextConverterApp.Data;

namespace TextConverterApp.Controllers;

public class TextConverterController : BaseController
{
    private readonly ITextConverterService _textConverterService;
    private readonly SessionHelper _sessionHelper;
    private readonly ConversionsDbContext _dbContext;


    public TextConverterController(ITextConverterService textConverterService, SessionHelper sessionHelper, ConversionsDbContext dbContext): base(sessionHelper)
    {
        _textConverterService = textConverterService;
        _sessionHelper = sessionHelper;
        _dbContext = dbContext;
    }
    
    public IActionResult Index()
    {
        var sessionId = _sessionHelper.GetSessionId();
        ViewBag.SessionId = sessionId;
        return View();
    }
    
    public async Task<IActionResult> MyConversions()
    {
        var sessionId = _sessionHelper.GetSessionId();
        var userId = Guid.Parse(sessionId);

        var conversions = await _dbContext.Conversions
            .Where(c => c.UserModel!.Id == userId)
            .ToListAsync();

        return View(conversions);
    }

    [Route("TextConverter/NotFound")]
    public IActionResult PageNotFound()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Convert(string inputText)
    {
        try
        {
            var result = await _textConverterService.ConvertToLeetSpeakAsync(inputText);
            var sessionId = _sessionHelper.GetSessionId();
            
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(sessionId));
            if (user == null)
            {
                user = new UserModel { Id = Guid.Parse(sessionId) };
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }

            var conversion = new ConversionModel
            {
                Id = Guid.NewGuid(),
                Before = inputText,
                After = result,
                UserModel = user
            };

            _dbContext.Conversions.Add(conversion);
            await _dbContext.SaveChangesAsync();

            return Json(new { translatedText = result });
        }
        catch (HttpRequestException ex)
        {
            var errorJson = JObject.Parse(ex.Message);
            return Json(new
            {
                error = new
                {
                    code = (int)errorJson["error"]!["code"]!,
                    message = (string)errorJson["error"]!["message"]!
                }
            });
        }
    }
}