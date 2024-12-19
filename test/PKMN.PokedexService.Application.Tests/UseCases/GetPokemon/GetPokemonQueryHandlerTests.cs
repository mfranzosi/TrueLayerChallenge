using AutoBogus;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using PKMN.PokedexService.Application.Interfaces;
using PKMN.PokedexService.Application.Mappings;
using PKMN.PokedexService.Application.UseCases.GetPokemon;

namespace PKMN.PokedexService.Application.Tests.UseCases.GetPokemon
{
    public class GetPokemonQueryHandlerTests
    {
        private readonly IPokemonClient _pokemonClient;
        private readonly IMapper _mapper;
        private readonly GetPokemonQueryHandler _getPokemonQueryHandler;

        public GetPokemonQueryHandlerTests()
        {
            _pokemonClient = Substitute.For<IPokemonClient>();

            var mapperConfiguration = new MapperConfiguration(cfg =>
               cfg.AddProfile(new PokemonProfile()));
            _mapper = mapperConfiguration.CreateMapper();
            _getPokemonQueryHandler = new GetPokemonQueryHandler(_pokemonClient, _mapper);
        }

        [Fact]
        public async Task Returns_Pokemon_Successfully()
        {
            //Arrange
            var request = new GetPokemonQuery()
            {
                Name = AutoFaker.Generate<string>()
            };

            var pokemon = AutoFaker.Generate<Domain.Entities.Pokemon>();

            _pokemonClient.GetPokemonByName(request.Name)
                .Returns(pokemon);

            //Act
            var result = await _getPokemonQueryHandler.Handle(request);

            //Assert
            result.Should().NotBeNull().And.BeEquivalentTo(pokemon);
        }
    }
}
