using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class DEPENSE_VEHICULE
{
    public Guid ID_DEPENSE { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public Guid ID_VEHICULE { get; set; }

    public Guid? ID_INTERVENTION { get; set; }

    public Guid ID_SAISIE_PAR { get; set; }

    public string TYPE_DEPENSE { get; set; } = null!;

    public decimal MONTANT_XAF { get; set; }

    public DateTime DATE_DEPENSE { get; set; }

    public string? DESCRIPTION { get; set; }

    public int? KILOMETRAGE_AU_MOMENT { get; set; }

    public string? URL_JUSTIFICATIF { get; set; }

    public DateTime DATE_CREATION { get; set; }

    public DateTime? DATE_MODIFICATION { get; set; }

    public virtual ICollection<ENTRETIEN_VEHICULE> ENTRETIEN_VEHICULEs { get; set; } = new List<ENTRETIEN_VEHICULE>();

    public virtual INTERVENTION? ID_INTERVENTIONNavigation { get; set; }

    public virtual LOCATAIRE ID_LOCATAIRENavigation { get; set; } = null!;

    public virtual UTILISATEUR ID_SAISIE_PARNavigation { get; set; } = null!;

    public virtual VEHICULE ID_VEHICULENavigation { get; set; } = null!;

    public virtual REF_TYPE_DEPENSE_VEHICULE TYPE_DEPENSENavigation { get; set; } = null!;
}
