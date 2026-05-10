using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DepenseIntervention
    {
        public Guid? IdDepense { get; set; }
        public Guid IdIntervention { get; set; }
        public Guid? IdEmploye { get; set; }
        public Guid? SaisiPar { get; set; }
        public string Reference { get; set; }
        public string TypeDepense { get; set; }
        public string? Note { get; set; }
        public decimal Montant { get; set; }
        public DateTime? DateDepense { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
