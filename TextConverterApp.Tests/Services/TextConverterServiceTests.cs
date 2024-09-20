using System.Net;
using TextConverterApp.Services.TextConverterService;

namespace TextConverterApp.Tests;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;


public class TextConverterServiceTests
{
    [Fact]
    public async Task ConvertToLeetSpeakAsync_ReturnsTranslatedText()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"contents\":{\"translated\":\"l33t sp34k\"}}"),
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new TextConverterServiceImpl(httpClient);

        // Act
        var result = await service.ConvertToLeetSpeakAsync("test");

        // Assert
        Assert.Equal("l33t sp34k", result);
    }
}