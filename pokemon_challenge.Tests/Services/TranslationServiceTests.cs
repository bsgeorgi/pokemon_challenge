using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using pokemon_challenge.Models;
using pokemon_challenge.Services;
using Shouldly;
using Xunit;

namespace pokemon_challenge.Tests.Services
{
    public class TranslationServiceTests
    {
        [Fact]
        public void GetTranslationAsync_Should_Return_Null_If_Translation_Param_Is_Empty()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var httpClientFactoryMock = fixture.Create<IHttpClientFactory>();
            var translationService = new TranslationService(httpClientFactoryMock);

            var result = translationService.GetTranslationAsync("", "");

            result.Result.ShouldBeNull();
        }

        [Fact]
        public void GetTranslationAsync_Should_Return_Null_If_Translation_Param_Incorrect()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var httpClientFactoryMock = fixture.Create<IHttpClientFactory>();
            var translationService = new TranslationService(httpClientFactoryMock);

            var result = translationService.GetTranslationAsync("", "myTranslation");

            result.Result.ShouldBeNull();
        }

        [Fact]
        public async Task GetTranslationAsync_Should_Return_Pokemon_Model_ResponseAsync()
        {
            const string responseString = "{\"success\":{\"total\":1},\"contents\":{\"translated\":\"Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.\",\"text\":\"It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.\",\"translation\":\"yoda\"}}";
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

            var translationService = new TranslationService(httpClientFactory);
            var result = await translationService.GetTranslationAsync("Text", "yoda");

            const string expectedTranslation =
                "Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.";
            
            result.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.ShouldBeOfType<TranslationModel>(),
                () => result.contents.translated.ShouldBe(expectedTranslation)
            );
        }

        [Fact]
        public async Task GetTranslationAsync_Should_Return_Null_If_Response_Is_Unsuccessful()
        {
            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.NotFound, "responseString");
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var httpClientFactory = httpClientFactoryMock.Object;

            var translationService = new TranslationService(httpClientFactory);
            var result = await translationService.GetTranslationAsync("Text", "yoda");
            
            result.ShouldBeNull();
        }
    }
}
