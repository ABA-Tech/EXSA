using Domain.Models;

namespace Domain.Stores
{
    public interface IEmployeStore
    {
        public Task<IEnumerable<Employe>> GetAllEmployeAsync();
        public Task<Employe> GetEmployeByIdAsync(Guid id);
        public Task<Employe> CreateEmployeAsync(Employe employe);
        public Task<Employe> UpdateEmployeAsync(Employe employe);
        public Task DeleteEmployeAsync(Employe employe);
        public Task DeleteEmployeByIdAsync(Guid id);
    }
}
