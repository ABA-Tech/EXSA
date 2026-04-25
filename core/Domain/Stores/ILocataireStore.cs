using Domain.Models;

namespace Domain.Stores
{
    public interface ILocataireStore
    {
        public Task<IEnumerable<Locataire>> GetAllLocataireAsync();
        public Task<Locataire> GetLocataireByIdAsync(Guid id);
        public Task<Locataire> CreateLocataireAsync(Locataire Locataire);
        public Task<Locataire> UpdateLocataireAsync(Locataire Locataire);
        public Task DeleteLocataireAsync(Locataire locataire);
        public Task DeleteLocataireByIdAsync(Guid id);
    }
}
