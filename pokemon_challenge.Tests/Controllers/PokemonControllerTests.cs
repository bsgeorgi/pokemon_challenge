using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using pokemon_challenge.Controllers;
using pokemon_challenge.Interfaces;
using pokemon_challenge.Models;
using Shouldly;
using Xunit;

namespace pokemon_challenge.Tests.Controllers
{
    public class PokemonControllerTests
    {
        [Fact]
        public async Task Get_Returns_Pokemon_Model()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var pokemonModel = fixture.Create<FormattedPokemonModel>();

            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock
                .Setup(service => service.GetBasicPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync(pokemonModel);

            var controller = new PokemonController(pokemonServiceMock.Object);

            var result = await controller.GetBasicPokemon("");
            var okResult = result as OkObjectResult;

            okResult.ShouldSatisfyAllConditions(
                () => okResult.ShouldNotBeNull(),
                () => okResult.StatusCode.ShouldBe(200)
            );
        }

        [Fact]
        public async Task Get_Returns_NotFound_If_Pokemon_Not_Found()
        {
            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock
                .Setup(service => service.GetBasicPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync(value: null);

            var controller = new PokemonController(pokemonServiceMock.Object);

            var result = await controller.GetBasicPokemon("");
            var badResult = result as NotFoundResult;

            badResult.ShouldSatisfyAllConditions(
                () => badResult.ShouldNotBeNull(),
                () => badResult.StatusCode.ShouldBe(404)
            );
        }

        [Fact]
        public async Task Translated_Get_Returns_Translated_Pokemon_Model()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var pokemonModel = fixture.Create<FormattedPokemonModel>();

            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock
                .Setup(service => service.GetTranslatedPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync(pokemonModel);

            var controller = new PokemonController(pokemonServiceMock.Object);

            var result = await controller.GetTranslatedPokemon("");
            var okResult = result as OkObjectResult;

            okResult.ShouldSatisfyAllConditions(
                () => okResult.ShouldNotBeNull(),
                () => okResult.StatusCode.ShouldBe(200)
            );
        }

        [Fact]
        public async Task Translated_Get_Returns_NotFound_If_Pokemon_Not_Found()
        {
            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock
                .Setup(service => service.GetTranslatedPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync(value: null);

            var controller = new PokemonController(pokemonServiceMock.Object);

            var result = await controller.GetTranslatedPokemon("");
            var badResult = result as NotFoundResult;

            badResult.ShouldSatisfyAllConditions(
                () => badResult.ShouldNotBeNull(),
                () => badResult.StatusCode.ShouldBe(404)
            );
        }
    }
}
