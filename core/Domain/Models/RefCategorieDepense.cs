using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RefCategorieDepense
    {
        public string Code { get; set; } = null!;

        public string Libelle { get; set; } = null!;
        public bool? Actif { get; set; }
    }

}
