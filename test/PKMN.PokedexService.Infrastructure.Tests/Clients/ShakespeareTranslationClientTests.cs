using AutoBogus;
using FluentAssertions;
using NSubstitute;
using PKMN.PokedexService.Infrastructure.Clients;
using PKMN.PokedexService.Infrastructure.Interfaces;

namespace PKMN.PokedexService.Infrastructure.Tests.Clients
{
    public class ShakespeareTranslationClientTests
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly ShakespeareTranslationClient _shakespeareTranslationClient;

        public ShakespeareTranslationClientTests()
        {
            _httpClient = Substitute.For<IHttpClientWrapper>();
            _shakespeareTranslationClient = new ShakespeareTranslationClient(_httpClient);
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
                        "translated": "'t hath the ability to sense the auras of all things. 't understands human speech.",
                        "text": "It has the ability to sense the\nauras of all things.\nIt understands human speech.",
                        "translation": "shakespeare"
                    }
                }
                """;

            var expectedTranslation = "'t hath the ability to sense the auras of all things. 't understands human speech.";

            _httpClient.PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>())
                .Returns(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse)
                });

            //Act
            var result = await _shakespeareTranslationClient.GetTranslation(text);

            //Assert
            result.Should().NotBeNull().And.Be(expectedTranslation);
        }
    }
}
