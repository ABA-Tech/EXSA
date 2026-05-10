using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Outputs;
using Domain.Services;
using Domain.Stores;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Application.Services
{
    public class InterventionService : AppService<Intervention>, IInterventionService
    {
        private readonly IInterventionStore _interventionStore;
        public InterventionService(IInterventionStore interventionStore): base(interventionStore)
        {
            _interventionStore = interventionStore;
        }

        public async Task<AffectationIntervention> CreateAffectationInterventionAsync(AffectationIntervention affectationIntervention)
        {
            return await _interventionStore.AddAffectationInterventionAsync(affectationIntervention);
        }

        public async Task<bool> CreateDepenseInterventionAsync(SaisieDepenseInterventionDto saisieDepenseIntervention)
        {
            var intervention = await _interventionStore.GetByIdAsync(saisieDepenseIntervention.IdIntervention);
            if(intervention == null)
            {
                throw new ApplicationException("Id Intervention non valide");
            }

            var lignesAffectation = saisieDepenseIntervention.Techniciens;
            var counter = 0;
            if(lignesAffectation != null && lignesAffectation.Any())
            {
                foreach(var tecnicien in lignesAffectation)
                {
                    var res = await _interventionStore.AddDepenseIntervention(new DepenseIntervention
                    {
                        DateCreation = DateTime.Now,
                        DateDepense = saisieDepenseIntervention.Date,
                        IdEmploye = tecnicien.IdTechnicien,
                        Montant = saisieDepenseIntervention.Montant,
                        IdIntervention = saisieDepenseIntervention.IdIntervention,
                        Note = saisieDepenseIntervention.Note,
                        Reference = saisieDepenseIntervention.Reference,
                        SaisiPar = saisieDepenseIntervention.SaisiPar,
                        TypeDepense = saisieDepenseIntervention.TypeDepense
                    });
                    counter += res;
                }
            }

            return lignesAffectation != null && counter == lignesAffectation.Count();
        }

        public async Task<IEnumerable<AffectationIntervention>> GetAllAffectationsAsync(Guid IdIntervention)
        {
            return await _interventionStore.GetAffectationsAsync(IdIntervention);
        }

        public async Task<IEnumerable<PhotoIntervention>> GetAllPhotoInterventionAsync(Guid idIntervention)
        {
            return await _interventionStore.GetPhotoInterventionAsync(idIntervention);
        }

        public async Task RemoveAffectationAsync(AffectationIntervention affectation)
        {
            await _interventionStore.RemoveAffectationAsync(affectation);
        }

        public async Task RemovePhotoInterventionAsync(Guid idPhotoIntervention)
        {
            var photoIntervention = await _interventionStore.GetPhotoInterventionByIdAsync(idPhotoIntervention);
            if (photoIntervention == null)
            {
                throw new ApplicationException("Photo inexistante");
            }


            var fileName = Path.GetFileName(photoIntervention.UrlBlob);
            var folder = photoIntervention.IdIntervention.ToString();

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", folder, fileName);

            // Supprimer fichier physique
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }


            await _interventionStore.DeletePhotoInterventionAsync(photoIntervention);
        }

        public async Task<bool> UploadPhotoInterventionAsync(PhotoIntervention intervention)
        {
            return await _interventionStore.UploadPhotoInterventionAsync(intervention);
        }

        private static RationTransportGridDto BuildGrid(List<RationTransportOutput> rows)
        {
            var result = new RationTransportGridDto();

            if (rows == null || rows.Count == 0)
                return result;

            var techniciens = rows
                .GroupBy(x => x.ID_EMPLOYE)
                .Select(g =>
                {
                    var first = g.First();

                    return new TechnicienColumnDto
                    {
                        IdEmploye = first.ID_EMPLOYE,
                        NomComplet = first.NOM_COMPLET,
                        NumeroEmploye = first.NUMERO_EMPLOYE,
                        EstPrincipal = first.EST_PRINCIPAL,
                        Initiales = GetInitiales(first.NOM_COMPLET)
                    };
                })
                .OrderByDescending(x => x.EstPrincipal)
                .ThenBy(x => x.NomComplet)
                .ToList();

            var jours = rows
                .GroupBy(x => x.JOUR.Date)
                .OrderBy(g => g.Key)
                .Select(dayGroup =>
                {
                    var firstDayRow = dayGroup.First();

                    var jourDto = new JourRationTransportDto
                    {
                        Date = dayGroup.Key,
                        LibelleJour = GetJourCourt(dayGroup.Key),
                        LibelleDate = dayGroup.Key.ToString("d MMMM", new CultureInfo("fr-FR")),

                        SousTotalRationsJour = firstDayRow.SOUS_TOTAL_RATIONS_JOUR,
                        SousTotalTransportJour = firstDayRow.SOUS_TOTAL_TRANSPORT_JOUR,
                        SousTotalJour = firstDayRow.SOUS_TOTAL_JOUR
                    };

                    foreach (var tech in techniciens)
                    {
                        var row = dayGroup.FirstOrDefault(x => x.ID_EMPLOYE == tech.IdEmploye);

                        if (row == null)
                        {
                            jourDto.Cellules.Add(new CelluleRationTransportDto
                            {
                                IdEmploye = tech.IdEmploye,
                                MontantRation = null,
                                MontantTransport = null,
                                IdDepenseRation = null,
                                IdDepenseTransport = null,
                                TotalCase = 0
                            });

                            continue;
                        }

                        jourDto.Cellules.Add(new CelluleRationTransportDto
                        {
                            IdEmploye = row.ID_EMPLOYE,
                            MontantRation = row.MONTANT_RATION,
                            MontantTransport = row.MONTANT_TRANSPORT,
                            IdDepenseRation = row.ID_DEPENSE_RATION,
                            IdDepenseTransport = row.ID_DEPENSE_TRANSPORT,
                            IdDepense = row.ID_DEPENSE,
                            TotalCase = row.TOTAL_CASE
                        });
                    }

                    return jourDto;
                })
                .ToList();

            var sousTotauxTechniciens = techniciens
                .Select(tech =>
                {
                    var row = rows.FirstOrDefault(x => x.ID_EMPLOYE == tech.IdEmploye);

                    return new SousTotalTechnicienDto
                    {
                        IdEmploye = tech.IdEmploye,

                        SousTotalRations = row?.SOUS_TOTAL_RATIONS_TECH ?? 0,
                        SousTotalTransport = row?.SOUS_TOTAL_TRANSPORT_TECH ?? 0,
                        SousTotal = row?.SOUS_TOTAL_TECH ?? 0,

                        NbJoursAvecRation = row?.NB_JOURS_AVEC_RATION ?? 0,
                        NbJoursAvecTransport = row?.NB_JOURS_AVEC_TRANSPORT ?? 0
                    };
                })
                .ToList();

            result.Techniciens = techniciens;
            result.Jours = jours;
            result.SousTotauxTechniciens = sousTotauxTechniciens;
            result.GrandTotal = rows.First().GRAND_TOTAL_INTERVENTION;

            return result;
        }

        private static string GetInitiales(string nomComplet)
        {
            if (string.IsNullOrWhiteSpace(nomComplet))
                return string.Empty;

            var parts = nomComplet
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
                return parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpper();

            return string.Concat(parts.Take(2).Select(x => x[0])).ToUpper();
        }

        private static string GetJourCourt(DateTime date)
        {
            return date.ToString("ddd", new CultureInfo("fr-FR"))
                .Replace(".", "")
                .ToTitleCase();
        }
        public async Task<RationTransportGridDto> GetRationTransportGridAsync(Guid idIntervention)
        {
            var rows = await _interventionStore.GetSaisiesRationTransportAsync(idIntervention);
            return BuildGrid(rows.ToList());
        }

        public async Task<RationTransportGridDto> GetGrilleRationTransportAsync(Guid idIntervention)
        {
            try
            {
                var rows = await _interventionStore.GetSaisiesRationTransportAsync(idIntervention);
                return BuildGrid(rows.ToList());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateDepenseIntervention(Guid idIntervention, PathDepenseInterventionDto depenseInterventionDto)
        {
            if(depenseInterventionDto == null || !depenseInterventionDto.IdDepense.HasValue)
            {
                throw new ApplicationException("Les données envoyé ne sont pas correctes");
            }

            var depenseIntervention = await _interventionStore.GetDepenseRationTransportById(depenseInterventionDto.IdDepense.Value);

            if (depenseIntervention == null || depenseIntervention.IdIntervention != idIntervention)
            {
                throw new ApplicationException("La dépense saisie n'existe pas");
            }

            if (depenseIntervention.TypeDepense.ToLower() == depenseInterventionDto.TypeDepense.ToLower())
            {
                depenseIntervention.Montant = depenseInterventionDto.Montant;

                return await _interventionStore.UpdateDepenseRationTransport(depenseIntervention);
            }

            var newDepense = new DepenseIntervention
            {
                Montant = depenseInterventionDto.Montant,
                DateCreation = DateTime.Now,
                DateDepense = depenseIntervention.DateDepense,
                IdEmploye = depenseInterventionDto.IdTechnicien,
                Note = depenseInterventionDto.Note,
                IdIntervention = depenseIntervention.IdIntervention,
                Reference = depenseIntervention.Reference,
                TypeDepense = depenseInterventionDto.TypeDepense
            };
            var res =  await _interventionStore.AddDepenseIntervention(newDepense, true);

            return res > 0;
        }
    }

    public static class StringExtensions
    {
        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            return char.ToUpper(value[0]) + value.Substring(1);
        }
    }
}
