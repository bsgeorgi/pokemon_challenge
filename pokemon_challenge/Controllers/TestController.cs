using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pokemon_challenge.Models;
using pokemon_challenge.Services;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace pokemon_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ITranslationService _translationService;

        public TestController(ILogger<TestController> logger, ITranslationService translationService)
        {
            _logger = logger;
            _translationService = translationService;
        }

        [HttpGet]
        [ResponseType(typeof(TranslationObject))]
        public async Task<TranslationObject> GetYodaTranslation()
        {
            var text = "Lost a planet,  master obiwan has.";
            var response = await _translationService.GetYodaTranslationObjectAsync(text);

            return response;
        }
    }
}
