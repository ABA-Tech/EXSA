using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class V_DEPENSES_PAR_VEHICULE
{
    public Guid ID_DEPENSE { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public string IMMATRICULATION { get; set; } = null!;

    public string VEHICULE { get; set; } = null!;

    public string TYPE_DEPENSE { get; set; } = null!;

    public string TYPE_DEPENSE_LIBELLE { get; set; } = null!;

    public bool EST_DEDUCTIBLE { get; set; }

    public decimal MONTANT_XAF { get; set; }

    public DateTime DATE_DEPENSE { get; set; }

    public string? DESCRIPTION { get; set; }

    public int? KILOMETRAGE_AU_MOMENT { get; set; }

    public string? URL_JUSTIFICATIF { get; set; }

    public Guid? ID_INTERVENTION { get; set; }

    public string? INTERVENTION_REFERENCE { get; set; }

    public string? INTERVENTION_TITRE { get; set; }

    public string CONTEXTE { get; set; } = null!;

    public string SAISIE_PAR { get; set; } = null!;

    public DateTime DATE_CREATION { get; set; }
}
