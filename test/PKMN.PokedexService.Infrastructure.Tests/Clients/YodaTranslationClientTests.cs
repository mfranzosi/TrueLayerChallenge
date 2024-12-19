using AutoBogus;
using FluentAssertions;
using NSubstitute;
using PKMN.PokedexService.Infrastructure.Clients;
using PKMN.PokedexService.Infrastructure.Interfaces;

namespace PKMN.PokedexService.Infrastructure.Tests.Clients
{
    public class YodaTranslationClientTests
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly YodaTranslationClient _yodaTranslationClient;

        public YodaTranslationClientTests()
        {
            _httpClient = Substitute.For<IHttpClientWrapper>();
            _yodaTranslationClient = new YodaTranslationClient(_httpClient);
        }

        [Fact]
        public async Task Returns_Translation_Successfully()
        {
            //Arrange
            var text = AutoFaker.Generate<string>();
            var jsonResponse = """
                {
                    "success": {
                        "total": 1
                    },
                    "contents": {
                        "translated": "Said to live in another world,  it is.From the surface of its body can make the darkest of nights light up like midday,  the intense light it radiates.",
                        "text": "It is said to live in another world. The intense\nlight it radiates from the surface of its body can\nmake the darkest of nights light up like midday.",
                        "translation": "yoda"
                    }
                }
                """;

            var expectedTranslation = "Said to live in another world,  it is.From the surface of its body can make the darkest of nights light up like midday,  the intense light it radiates.";

            _httpClient.PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>())
                .Returns(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse)
                });

            //Act
            var result = await _yodaTranslationClient.GetTranslation(text);

            //Assert
            result.Should().NotBeNull().And.Be(expectedTranslation);
        }
    }
}
