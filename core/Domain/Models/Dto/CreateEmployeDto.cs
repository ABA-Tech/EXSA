using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto
{
    public class CreateEmployeDto
    {
        public Guid IdLocataire { get; set; }

        public string NomComplet { get; set; } = null!;

        public string Telephone { get; set; } = null!;

        public string? Email { get; set; }

        public string MotDePasseHash { get; set; } = "";

        public string Role { get; set; } = "";

        public string? TokenFcm { get; set; }

        public bool? EstActif { get; set; } = true;

        public bool EstSupprime { get; set; } = false;

        public string NumeroEmploye { get; set; } = null!;

        public decimal SalaireBaseXaf { get; set; }

        public string TypeContrat { get; set; } = null!;

        public DateTime DateEmbauche { get; set; }

        public string? NumeroCnps { get; set; }

        public Guid? IdEmploye { get; set; }
        public Guid? IdUtilisateur { get; set; }
    }
}
