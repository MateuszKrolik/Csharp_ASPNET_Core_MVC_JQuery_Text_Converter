using Newtonsoft.Json.Linq;

namespace TextConverterApp.Services.TextConverterService;

public class TextConverterServiceImpl : ITextConverterService
{
    private readonly HttpClient _httpClient;

    public TextConverterServiceImpl(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ConvertToLeetSpeakAsync(string input)
    {
        var response = await _httpClient.GetAsync($"https://api.funtranslations.com/translate/leetspeak.json?text={Uri.EscapeDataString(input)}");

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(errorResponse);
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(responseString);
        return json["contents"]!["translated"]!.ToString();
    }
}