using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
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
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var httpResponseMessageMock = fixture.Create<HttpResponseMessage>();

            var expected = "Hello World";
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                var response = request.CreateResponse(HttpStatusCode.OK, expected);
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var httpClientFactory = httpClientFactoryMock.Object;

            var translationService = new TranslationService(httpClientFactory);
            var result = await translationService.GetTranslationAsync("This is my text", "yoda");

            result.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull()
            );
        }
    }
}
