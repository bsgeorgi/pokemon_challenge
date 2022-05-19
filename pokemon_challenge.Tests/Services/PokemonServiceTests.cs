using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using pokemon_challenge.Models;
using pokemon_challenge.Services;
using Shouldly;
using Xunit;

namespace pokemon_challenge.Tests.Services
{
    public class PokemonServiceTests
    {
        [Fact]
        public async Task RetrievePokemonDataAsync_Should_Return_PokemonModel()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var loggerMock = new Mock<ILogger<PokemonService>>();
            var translationServiceMock = fixture.Create<TranslationService>();

            var name = fixture.Create<string>();
            var description = fixture.Create<string>();
            var habitat = fixture.Create<string>();
            var responseString = "{\"base_happiness\":0,\"capture_rate\":3,\"color\":{},\"egg_groups\":[],\"evolution_chain\":{},\"evolves_from_species\":null,\"flavor_text_entries\":[{\"flavor_text\":\"" + description + "\",\"language\":{\"name\":\"en\",\"url\":\"url\"},\"version\":{\"name\":\"red\",\"url\":\"url\"}}],\"form_descriptions\":[],\"forms_switchable\":true,\"gender_rate\":-1,\"genera\":[],\"generation\":{},\"growth_rate\":{},\"habitat\":{\"name\":\"" + habitat + "\",\"url\":\"url\"},\"has_gender_differences\":false,\"hatch_counter\":120,\"id\":150,\"is_baby\":false,\"is_legendary\":true,\"is_mythical\":false,\"name\":\"" + name + "\",\"names\":[],\"order\":182,\"pal_park_encounters\":[],\"pokedex_numbers\":[],\"shape\":{\"name\":\"upright\",\"url\":\"https://pokeapi.co/api/v2/pokemon-shape/6/\"},\"varieties\":[]}";

            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.OK, responseString);
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var httpClientFactory = httpClientFactoryMock.Object;

            var pokemonService = new PokemonService(loggerMock.Object,
                httpClientFactory, translationServiceMock);

            var result = await pokemonService.RetrievePokemonDataAsync("");
            result.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.ShouldBeOfType<PokemonModel>(),
                () => result.name.ShouldBe(name),
                () => result.flavor_text_entries.First().flavor_text.ShouldBe(description),
                () => result.habitat.name.ShouldBe(habitat)
            );
        }

        [Fact]
        public async Task RetrievePokemonDataAsync_Should_Return_Null_If_Response_Is_Unsuccessful()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.NotFound, "");
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var httpClientFactory = httpClientFactoryMock.Object;

            var loggerMock = new Mock<ILogger<PokemonService>>();
            var translationServiceMock = fixture.Create<TranslationService>();

            var pokemonService = new PokemonService(loggerMock.Object,
                httpClientFactory, translationServiceMock);

            var result = await pokemonService.RetrievePokemonDataAsync("");
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetBasicPokemonAsync_Should_Return_Beautified_Pokemon_Model()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var loggerMock = new Mock<ILogger<PokemonService>>();
            var translationServiceMock = fixture.Create<TranslationService>();

            var name = fixture.Create<string>();
            var description = fixture.Create<string>();
            var habitat = fixture.Create<string>();
            var responseString = "{\"base_happiness\":0,\"capture_rate\":3,\"color\":{},\"egg_groups\":[],\"evolution_chain\":{},\"evolves_from_species\":null,\"flavor_text_entries\":[{\"flavor_text\":\"" + description + "\",\"language\":{\"name\":\"en\",\"url\":\"url\"},\"version\":{\"name\":\"red\",\"url\":\"url\"}}],\"form_descriptions\":[],\"forms_switchable\":true,\"gender_rate\":-1,\"genera\":[],\"generation\":{},\"growth_rate\":{},\"habitat\":{\"name\":\"" + habitat + "\",\"url\":\"url\"},\"has_gender_differences\":false,\"hatch_counter\":120,\"id\":150,\"is_baby\":false,\"is_legendary\":true,\"is_mythical\":false,\"name\":\"" + name + "\",\"names\":[],\"order\":182,\"pal_park_encounters\":[],\"pokedex_numbers\":[],\"shape\":{\"name\":\"upright\",\"url\":\"https://pokeapi.co/api/v2/pokemon-shape/6/\"},\"varieties\":[]}";

            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.OK, responseString);
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var httpClientFactory = httpClientFactoryMock.Object;

            var pokemonService = new PokemonService(loggerMock.Object,
                httpClientFactory, translationServiceMock);

            var result = await pokemonService.GetBasicPokemonAsync("");
            result.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.ShouldBeOfType<FormattedPokemonModel>()
            );
        }

        [Fact]
        public async Task GetBasicPokemonAsync_Should_Return_Null_If_Pokemon_Model_Is_Null()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var loggerMock = new Mock<ILogger<PokemonService>>();
            var translationServiceMock = fixture.Create<TranslationService>();

            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.OK, "");
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var httpClientFactory = httpClientFactoryMock.Object;

            var pokemonService = new PokemonService(loggerMock.Object,
                httpClientFactory, translationServiceMock);

            var result = await pokemonService.GetBasicPokemonAsync("");
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetTranslatedPokemonAsync_Should_Return_Null_If_Pokemon_Model_Is_Null()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var loggerMock = new Mock<ILogger<PokemonService>>();
            var translationServiceMock = fixture.Create<TranslationService>();

            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.OK, "");
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var httpClientFactory = httpClientFactoryMock.Object;

            var pokemonService = new PokemonService(loggerMock.Object,
                httpClientFactory, translationServiceMock);

            var result = await pokemonService.GetTranslatedPokemonAsync("");
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetTranslatedPokemonAsync_Should_Return_Translated_Using_Yoda()
        {
            var loggerMock = new Mock<ILogger<PokemonService>>();
            const string responseString = "{\"base_happiness\":0,\"capture_rate\":3,\"color\":{},\"egg_groups\":[],\"evolution_chain\":{},\"evolves_from_species\":null,\"flavor_text_entries\":[{\"flavor_text\":\"xx\",\"language\":{\"name\":\"en\",\"url\":\"url\"},\"version\":{\"name\":\"red\",\"url\":\"url\"}}],\"form_descriptions\":[],\"forms_switchable\":true,\"gender_rate\":-1,\"genera\":[],\"generation\":{},\"growth_rate\":{},\"habitat\":{\"name\":\"cave\",\"url\":\"url\"},\"has_gender_differences\":false,\"hatch_counter\":120,\"id\":150,\"is_baby\":false,\"is_legendary\":true,\"is_mythical\":false,\"name\":\"name\",\"names\":[],\"order\":182,\"pal_park_encounters\":[],\"pokedex_numbers\":[],\"shape\":{\"name\":\"upright\",\"url\":\"https://pokeapi.co/api/v2/pokemon-shape/6/\"},\"varieties\":[]}";

            // HttpClient for PokemonService
            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.OK, responseString);
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var httpClientFactory = httpClientFactoryMock.Object;

            // HttpClient for TranslationService
            const string responseStringT = "{\"success\":{\"total\":1},\"contents\":{\"translated\":\"Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.\",\"text\":\"It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.\",\"translation\":\"yoda\"}}";
            var configurationT = new HttpConfiguration();
            var clientHandlerStubT = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configurationT);
                var response = request.CreateResponse(HttpStatusCode.OK, responseStringT);
                return Task.FromResult(response);
            });
            var clientT = new HttpClient(clientHandlerStubT);

            var httpClientFactoryMockT = new Mock<IHttpClientFactory>();
            httpClientFactoryMockT
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(clientT);
            var httpClientFactoryT = httpClientFactoryMockT.Object;

            var translationService = new TranslationService(httpClientFactoryT);
            var pokemonService = new PokemonService(loggerMock.Object,
                httpClientFactory, translationService);

            const string expectedTranslation =
                "Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.";

            var result = await pokemonService.GetTranslatedPokemonAsync("");
            result.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.description.ShouldBe(expectedTranslation)
            );
        }

        [Fact]
        public async Task GetTranslatedPokemonAsync_Should_Return_Translated_Using_Shakespeare()
        {
            var loggerMock = new Mock<ILogger<PokemonService>>();
            const string responseString = "{\"base_happiness\":0,\"capture_rate\":3,\"color\":{},\"egg_groups\":[],\"evolution_chain\":{},\"evolves_from_species\":null,\"flavor_text_entries\":[{\"flavor_text\":\"xx\",\"language\":{\"name\":\"en\",\"url\":\"url\"},\"version\":{\"name\":\"red\",\"url\":\"url\"}}],\"form_descriptions\":[],\"forms_switchable\":true,\"gender_rate\":-1,\"genera\":[],\"generation\":{},\"growth_rate\":{},\"habitat\":{\"name\":\"xx\",\"url\":\"url\"},\"has_gender_differences\":false,\"hatch_counter\":120,\"id\":150,\"is_baby\":false,\"is_legendary\":false,\"is_mythical\":false,\"name\":\"111,\",\"names\":[],\"order\":182,\"pal_park_encounters\":[],\"pokedex_numbers\":[],\"shape\":{\"name\":\"upright\",\"url\":\"https://pokeapi.co/api/v2/pokemon-shape/6/\"},\"varieties\":[]}";

            // HttpClient for PokemonService
            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.OK, responseString);
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var httpClientFactory = httpClientFactoryMock.Object;

            // HttpClient for TranslationService
            const string responseStringT = "{\"success\":{\"total\":1},\"contents\":{\"translated\":\"Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.\",\"text\":\"It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.\",\"translation\":\"shakespeare\"}}";
            var configurationT = new HttpConfiguration();
            var clientHandlerStubT = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configurationT);
                var response = request.CreateResponse(HttpStatusCode.OK, responseStringT);
                return Task.FromResult(response);
            });
            var clientT = new HttpClient(clientHandlerStubT);

            var httpClientFactoryMockT = new Mock<IHttpClientFactory>();
            httpClientFactoryMockT
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(clientT);
            var httpClientFactoryT = httpClientFactoryMockT.Object;

            var translationService = new TranslationService(httpClientFactoryT);
            var pokemonService = new PokemonService(loggerMock.Object,
                httpClientFactory, translationService);

            const string expectedTranslation =
                "Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.";

            var result = await pokemonService.GetTranslatedPokemonAsync("");
            result.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.description.ShouldBe(expectedTranslation)
            );
        }

        [Fact]
        public async Task GetTranslatedPokemonAsync_Should_Log_If_Translation_Model_Is_Empty()
        {
            var loggerMock = new Mock<ILogger<PokemonService>>();

            const string responseString = "{\"base_happiness\":0,\"capture_rate\":3,\"color\":{},\"egg_groups\":[],\"evolution_chain\":{},\"evolves_from_species\":null,\"flavor_text_entries\":[{\"flavor_text\":\"xx\",\"language\":{\"name\":\"en\",\"url\":\"url\"},\"version\":{\"name\":\"red\",\"url\":\"url\"}}],\"form_descriptions\":[],\"forms_switchable\":true,\"gender_rate\":-1,\"genera\":[],\"generation\":{},\"growth_rate\":{},\"habitat\":{\"name\":\"xx\",\"url\":\"url\"},\"has_gender_differences\":false,\"hatch_counter\":120,\"id\":150,\"is_baby\":false,\"is_legendary\":false,\"is_mythical\":false,\"name\":\"111,\",\"names\":[],\"order\":182,\"pal_park_encounters\":[],\"pokedex_numbers\":[],\"shape\":{\"name\":\"upright\",\"url\":\"https://pokeapi.co/api/v2/pokemon-shape/6/\"},\"varieties\":[]}";

            // HttpClient for PokemonService
            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.OK, responseString);
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var httpClientFactory = httpClientFactoryMock.Object;

            // HttpClient for TranslationService
            var configurationT = new HttpConfiguration();
            var clientHandlerStubT = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configurationT);
                var response = request.CreateResponse(HttpStatusCode.OK, "");
                return Task.FromResult(response);
            });
            var clientT = new HttpClient(clientHandlerStubT);

            var httpClientFactoryMockT = new Mock<IHttpClientFactory>();
            httpClientFactoryMockT
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(clientT);
            var httpClientFactoryT = httpClientFactoryMockT.Object;

            var translationService = new TranslationService(httpClientFactoryT);
            var pokemonService = new PokemonService(loggerMock.Object,
                httpClientFactory, translationService);

            var result = await pokemonService.GetTranslatedPokemonAsync("");
            result.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.description.ShouldBe("xx")
            );
            loggerMock.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}
