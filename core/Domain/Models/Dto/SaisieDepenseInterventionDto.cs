using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto
{
    public class SaisieDepenseInterventionDto
    {
        public long Montant { get; set; }
        public string TypeDepense { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public string Reference { get; set; }
        public  Guid IdIntervention { get; set; }
        public  Guid? SaisiPar { get; set; }
        public List<AffectationIntervention>? Techniciens { get; set; } = new List<AffectationIntervention>();
    }

    public class PathDepenseInterventionDto
    {
        public long Montant { get; set;}
        public string TypeDepense { get; set;}
        public DateTime? Date { get; set; }
        public string? Note { get; set; }
        public Guid? IdTechnicien { get; set; }
        public Guid? IdDepense { get; set; }
    }

}
