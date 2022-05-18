using pokemon_challenge.Models;
using System.Threading.Tasks;

namespace pokemon_challenge.Services
{
    public interface ITranslationService
    {
        Task<TranslationModel> GetTranslationAsync(string text);
    }
}