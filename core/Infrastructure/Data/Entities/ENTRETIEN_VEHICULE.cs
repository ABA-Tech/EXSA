using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class ENTRETIEN_VEHICULE
{
    public Guid ID_ENTRETIEN { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public Guid ID_VEHICULE { get; set; }

    public Guid? ID_DEPENSE { get; set; }

    public string TYPE_ENTRETIEN { get; set; } = null!;

    public DateTime DATE_PREVUE { get; set; }

    public int? KILOMETRAGE_PREVU { get; set; }

    public string? PRESTATAIRE { get; set; }

    public DateTime? DATE_REALISE { get; set; }

    public int? KILOMETRAGE_REALISE { get; set; }

    public decimal? COUT_XAF { get; set; }

    public string STATUT { get; set; } = null!;

    public string? NOTES { get; set; }

    public Guid ID_CREATEUR { get; set; }

    public DateTime DATE_CREATION { get; set; }

    public DateTime? DATE_MODIFICATION { get; set; }

    public virtual UTILISATEUR ID_CREATEURNavigation { get; set; } = null!;

    public virtual DEPENSE_VEHICULE? ID_DEPENSENavigation { get; set; }

    public virtual LOCATAIRE ID_LOCATAIRENavigation { get; set; } = null!;

    public virtual VEHICULE ID_VEHICULENavigation { get; set; } = null!;

    public virtual REF_TYPE_ENTRETIEN TYPE_ENTRETIENNavigation { get; set; } = null!;
}
