using Domain.Models;
using Domain.Services;
using Domain.Stores;
using System.Linq.Expressions;

namespace Application.Services
{
    public class EmployeService : IGenericService<Employe>
    {
        public readonly IEmployeStore _employeStore;

        public EmployeService(IEmployeStore employeStore) 
        { 
            _employeStore = employeStore ?? throw new ApplicationException(nameof(employeStore));
        }

        public Task<bool> AnyAsync(Expression<Func<Employe, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Employe> CreateAsync(Employe entity, CancellationToken cancellationToken = default)
        {
            return await _employeStore.CreateEmployeAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var employe = await _employeStore.GetEmployeByIdAsync(id);
            if(employe == null)
            {
                throw new ApplicationException("Employé introuvable");
            }

            await _employeStore.DeleteEmployeAsync(employe);
            return true;
        }

        public async Task<IReadOnlyList<Employe>> FindAsync(Expression<Func<Employe, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Employe>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return (await _employeStore.GetAllEmployeAsync()).ToList();
        }

        public async Task<Employe?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _employeStore.GetEmployeByIdAsync(id);
        }

        public async Task UpdateAsync(Employe entity, CancellationToken cancellationToken = default)
        {
            var result = await _employeStore.UpdateEmployeAsync(entity);
        }
    }
}
