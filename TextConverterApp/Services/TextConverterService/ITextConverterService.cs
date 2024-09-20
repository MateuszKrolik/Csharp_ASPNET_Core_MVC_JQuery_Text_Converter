namespace TextConverterApp.Services.TextConverterService;

public interface ITextConverterService
{
    Task<string> ConvertToLeetSpeakAsync(string input);
}