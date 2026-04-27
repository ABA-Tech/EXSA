using Domain.Models;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Stores
{
    public class EmployeStore : IEmployeStore
    {
        private readonly ExsaDbContext _context;

        public EmployeStore(ExsaDbContext context) 
        {
            _context = context ?? throw new ApplicationException(nameof(context));
        }

        public async Task<Employe> CreateEmployeAsync(Employe employe)
        {
            var employeEntity = employe.ToEntity();
            _context.EMPLOYEs.Add(employeEntity);
            await _context.SaveChangesAsync();
            return employeEntity.ToModel();
        }

        public async Task DeleteEmployeAsync(Employe employe)
        {
            var employeEntity = employe.ToEntity();
            _context.EMPLOYEs.Remove(employeEntity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteEmployeByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employe>> GetAllEmployeAsync()
        {
            return (await _context.EMPLOYEs
                    .Include(x=>x.ID_UTILISATEURNavigation)
                    .ToListAsync())
                .ToModelCollection();
        }

        public async Task<Employe> GetEmployeByIdAsync(Guid id)
        {
            var employe = await _context.EMPLOYEs.Include(x => x.ID_UTILISATEURNavigation).AsNoTracking().FirstAsync(x => x.ID_EMPLOYE == id);
            return employe.ToModel();
        }

        public async Task<Employe> UpdateEmployeAsync(Employe employe)
        {
            var employeEntity = employe.ToEntity();
            _context.EMPLOYEs.Update(employeEntity);
            await _context.SaveChangesAsync();
            return employe;
        }
    }
}
