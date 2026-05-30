using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class FACTURE
{
    public Guid ID_FACTURE { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public Guid? ID_INTERVENTION { get; set; }

    public string REFERENCE { get; set; } = null!;

    public string STATUT { get; set; } = null!;

    public string NOM_CLIENT { get; set; } = null!;

    public decimal SOUS_TOTAL_XAF { get; set; }

    public decimal TAUX_TVA { get; set; }

    public decimal TOTAL_XAF { get; set; }

    public DateTime? DATE_ECHEANCE { get; set; }

    public DateTime? DATE_PAIEMENT { get; set; }

    public string? URL_PDF { get; set; }

    public DateTime DATE_CREATION { get; set; }

    public DateTime? DATE_MODIFICATION { get; set; }

    public virtual INTERVENTION? ID_INTERVENTIONNavigation { get; set; }

    public virtual LOCATAIRE ID_LOCATAIRENavigation { get; set; } = null!;

    public virtual ICollection<LIGNE_FACTURE> LIGNE_FACTUREs { get; set; } = new List<LIGNE_FACTURE>();

    public virtual ICollection<REGLEMENT> REGLEMENTs { get; set; } = new List<REGLEMENT>();

    public virtual REF_STATUT_FACTURE STATUTNavigation { get; set; } = null!;
}
