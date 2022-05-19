using pokemon_challenge.Models;
using System.Threading.Tasks;

namespace pokemon_challenge.Interfaces
{
    public interface ITranslationService
    {
        Task<TranslationModel> GetTranslationAsync(string text, string translation);
    }
}