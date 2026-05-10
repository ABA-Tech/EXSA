using Domain.Models;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Stores
{
    public class ReferentielStore : IReferentielStore
    {
        private readonly ExsaDbContext _context;
        public ReferentielStore(ExsaDbContext context) 
        { 
            _context = context ?? throw new ApplicationException(nameof(context));
        }
        public async Task<IEnumerable<RefTypeIntervention>> GetInterventionsAsync()
        {
            return (await _context.REF_TYPE_INTERVENTIONs.ToListAsync())
                .Select(x => new RefTypeIntervention
                {
                    Code = x.CODE,
                    Libelle = x.LIBELLE
                });
        }

        public async Task<IEnumerable<RefRole>> GetRolesAsync()
        {
            return (await _context.REF_ROLEs.ToListAsync())
                .Select(x => new RefRole
                {
                    Code = x.CODE,
                    Libelle = x.LIBELLE
                });
        }

        public async Task<IEnumerable<RefStatutFacture>> GetStatutFacturesAsync()
        {
            return (await _context.REF_STATUT_FACTUREs.ToListAsync())
                .Select(x => new RefStatutFacture
                {
                    Code = x.CODE,
                    Libelle = x.LIBELLE
                });
        }

        public async Task<IEnumerable<RefStatutIntervention>> GetStatutInterventionsAsync()
        {
            return (await _context.REF_STATUT_INTERVENTIONs.OrderBy(x=>x.ORDRE).ToListAsync())
                .Select(x => new RefStatutIntervention
                {
                    Code = x.CODE,
                    Ordre = x.ORDRE,
                    Libelle = x.LIBELLE
                });
        }

        public async Task<IEnumerable<RefTypeContrat>> GetTypeContratsAsync()
        {
            return (await _context.REF_TYPE_CONTRATs.ToListAsync())
                .Select(x=> new RefTypeContrat { Code = x.CODE, Libelle = x.LIBELLE });
        }

        public async Task<IEnumerable<RefTypeeDepenseIntervention>> GetTypeDepenseInterventionsAsync()
        {
            return (await _context.REF_TYPE_DEPENSE_INTERVENTIONs.OrderBy(x=>x.CODE).ToListAsync())
                .Select(x=> new RefTypeeDepenseIntervention { Code = x.CODE, Libelle =x.LIBELLE });
        }

        public async Task<IEnumerable<RefTypeMouvement>> GetTypeMouvementsAsync()
        {
            return (await _context.REF_TYPE_MOUVEMENTs.ToListAsync())
                .Select(x=> new RefTypeMouvement { Code = x.CODE, Libelle= x.LIBELLE });
        }

        public async Task<IEnumerable<RefTypePhoto>> GetTypePhotosAsync()
        {
            return (await _context.REF_TYPE_PHOTOs.ToListAsync()).Select(x=> new RefTypePhoto {  Code = x.CODE,Libelle =  x.LIBELLE });
        }

        public async Task<IEnumerable<RefTypePlan>> GetTypePlansAsync()
        {
            return (await _context.REF_TYPE_PLANs.ToListAsync())
                .Select(x => new RefTypePlan
                {
                    Libelle = x.LIBELLE,
                    Code = x.CODE,
                });
        }
    }
}
