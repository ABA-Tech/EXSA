using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class VEHICULE
{
    public Guid ID_VEHICULE { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public string IMMATRICULATION { get; set; } = null!;

    public string MARQUE { get; set; } = null!;

    public string MODELE { get; set; } = null!;

    public short ANNEE { get; set; }

    public string TYPE_VEHICULE { get; set; } = null!;

    public string? COULEUR { get; set; }

    public int KILOMETRAGE_ACTUEL { get; set; }

    public DateTime? DATE_ACQUISITION { get; set; }

    public decimal? PRIX_ACQUISITION_XAF { get; set; }

    public string? ASSURANCE_COMPAGNIE { get; set; }

    public string? ASSURANCE_NUMERO { get; set; }

    public DateTime? ASSURANCE_EXPIRATION { get; set; }

    public DateTime? VIGNETTE_EXPIRATION { get; set; }

    public DateTime? VISITE_TECHNIQUE_EXPIRATION { get; set; }

    public string STATUT { get; set; } = null!;

    public string? URL_PHOTO { get; set; }

    public string? NOTES { get; set; }

    public Guid ID_CREATEUR { get; set; }

    public DateTime DATE_CREATION { get; set; }

    public DateTime? DATE_MODIFICATION { get; set; }

    public bool EST_SUPPRIME { get; set; }

    public virtual ICollection<AFFECTATION_VEHICULE> AFFECTATION_VEHICULEs { get; set; } = new List<AFFECTATION_VEHICULE>();

    public virtual ICollection<DEPENSE_VEHICULE> DEPENSE_VEHICULEs { get; set; } = new List<DEPENSE_VEHICULE>();

    public virtual ICollection<ENTRETIEN_VEHICULE> ENTRETIEN_VEHICULEs { get; set; } = new List<ENTRETIEN_VEHICULE>();

    public virtual UTILISATEUR ID_CREATEURNavigation { get; set; } = null!;

    public virtual LOCATAIRE ID_LOCATAIRENavigation { get; set; } = null!;

    public virtual REF_STATUT_VEHICULE STATUTNavigation { get; set; } = null!;

    public virtual REF_TYPE_VEHICULE TYPE_VEHICULENavigation { get; set; } = null!;
}
