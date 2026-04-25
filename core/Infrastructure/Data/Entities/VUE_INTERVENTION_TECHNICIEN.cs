using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class VUE_INTERVENTION_TECHNICIEN
{
    public Guid ID_INTERVENTION { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public string REFERENCE { get; set; } = null!;

    public string TITRE { get; set; } = null!;

    public string TYPE { get; set; } = null!;

    public byte PRIORITE { get; set; }

    public string STATUT { get; set; } = null!;

    public string? NOM_CLIENT { get; set; }

    public DateTime? DATE_PLANIFIEE { get; set; }

    public DateTime? DATE_DEBUT { get; set; }

    public DateTime? DATE_FIN { get; set; }

    public DateTime? DATE_VALIDATION { get; set; }

    public string? NOM_TECHNICIEN_PRINCIPAL { get; set; }

    public string? TEL_TECHNICIEN_PRINCIPAL { get; set; }

    public string? NOM_CREATEUR { get; set; }
}
