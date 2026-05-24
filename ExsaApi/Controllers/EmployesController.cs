using Domain.Common;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExsaApi.Controllers
{
    [Route("api/[controller]")]

    [AllowAnonymous]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly IGenericService<Employe> _employeService;
        private readonly IAppService<Utilisateur> _utilisateurService;
        private readonly IGenericService<Locataire> _locataireService;
        private readonly IPasswordService _passwordService;

        public EmployesController(IGenericService<Employe> genericService, 
                                IAppService<Utilisateur> utilisateurService,
                                IGenericService<Locataire> locataireService,
                                IPasswordService passwordService)
        {
            _employeService = genericService;
            _utilisateurService = utilisateurService;
            _locataireService = locataireService;
            _passwordService = passwordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployes() 
        { 
            return Ok(await _employeService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmploye(Employe employe)
        {
            return Ok(await _employeService.CreateAsync(employe));
        }

        [HttpPost("CreateEmployeUser")]
        public async Task<IActionResult> CreateEmploye(CreateEmployeDto employe)
        {
            var locataire = (await _locataireService.GetAllAsync()).FirstOrDefault();
            if (locataire == null)
            {
                throw new Exception("Locataire non définit");
            }

            var utilisateur = new Utilisateur
            {
                DateCreation = DateTime.Now,
                DateModification = DateTime.Now,
                DerniereConnexion = null,
                Email = employe.Email,
                EstActif = true,
                NomComplet = employe.NomComplet,
                Telephone = employe.Telephone,
                IdLocataire = locataire.IdLocataire,
                MotDePasseHash = employe.MotDePasseHash,
                Role = employe.Role
            };
            utilisateur.MotDePasseHash = _passwordService.HashPassword(utilisateur, utilisateur.MotDePasseHash);

            var result = await _utilisateurService.CreateAsync(utilisateur);

            if (result == null)
                throw new Exception("Erreur de création de l'utilisateur");

            var nouveauEmploye = new Employe
            {
                IdLocataire = locataire.IdLocataire,
                DateCreation = DateTime.Now,
                DateModification = DateTime.Now,
                DateEmbauche = employe.DateEmbauche,
                EstActif = true,
                NumeroCnps = employe.NumeroCnps,
                NumeroEmploye = employe.NumeroEmploye,
                SalaireBaseXaf = employe.SalaireBaseXaf,
                TypeContrat = employe.TypeContrat,
                IdUtilisateur = result.IdUtilisateur
            };

            return Ok(await _employeService.CreateAsync(nouveauEmploye));
        }


        [HttpPost("UpdateEmployeUser")]
        public async Task<IActionResult> UpdateEmploye(CreateEmployeDto employe)
        {
            var locataire = (await _locataireService.GetAllAsync()).FirstOrDefault();
            if (locataire == null)
            {
                throw new Exception("Locataire non définit");
            }

            var utilisateur = await _utilisateurService.GetByIdAsync(employe.IdUtilisateur.Value);
            utilisateur.DateModification = DateTime.Now;
            utilisateur.Email = employe.Email;
            utilisateur.Telephone = employe.Telephone;
            utilisateur.NomComplet = employe.NomComplet;
            utilisateur.Role = employe.Role;
            utilisateur.MotDePasseHash = employe.MotDePasseHash;
            utilisateur.MotDePasseHash = _passwordService.HashPassword(utilisateur, utilisateur.MotDePasseHash);

            await _utilisateurService.UpdateAsync(utilisateur);

            var existingEmp = await _employeService.GetByIdAsync(employe.IdEmploye.Value);
            if (existingEmp == null)
            {
                throw new Exception("Employé non définit");
            }
            existingEmp.DateEmbauche = employe.DateEmbauche;
            existingEmp.DateModification = DateTime.Now;
            existingEmp.TypeContrat = employe.TypeContrat;
            existingEmp.NumeroCnps = employe.NumeroCnps;
            existingEmp.SalaireBaseXaf = employe.SalaireBaseXaf;
            existingEmp.NumeroEmploye = employe.NumeroEmploye;

            await _employeService.UpdateAsync(existingEmp);
            return Ok();
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeById(Guid Id)
        {
            return Ok(await _employeService.GetByIdAsync(Id));
        }
    }
}
