using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class INTERVENTION
{
    public Guid ID_INTERVENTION { get; set; }

    public string? ID_LOCAL { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public string REFERENCE { get; set; } = null!;

    public string TITRE { get; set; } = null!;

    public string? DESCRIPTION { get; set; }

    public string TYPE { get; set; } = null!;

    public byte PRIORITE { get; set; }

    public string STATUT { get; set; } = null!;

    public string? NOM_CLIENT { get; set; }

    public string? ADRESSE { get; set; }

    public decimal? LATITUDE { get; set; }

    public decimal? LONGITUDE { get; set; }

    public DateTime? DATE_PLANIFIEE { get; set; }

    public DateTime? DATE_DEBUT { get; set; }

    public DateTime? DATE_FIN { get; set; }

    public DateTime? DATE_VALIDATION { get; set; }

    public Guid? ID_VALIDATEUR { get; set; }

    public string? URL_SIGNATURE { get; set; }

    public string? NOTES { get; set; }

    public Guid ID_CREATEUR { get; set; }

    public DateTime DATE_CREATION { get; set; }

    public DateTime? DATE_MODIFICATION { get; set; }

    public bool EST_SUPPRIME { get; set; }

    public decimal? MONTANT_CONVENU_XAF { get; set; }

    public virtual ICollection<AFFECTATION_INTERVENTION> AFFECTATION_INTERVENTIONs { get; set; } = new List<AFFECTATION_INTERVENTION>();

    public virtual ICollection<AFFECTATION_VEHICULE> AFFECTATION_VEHICULEs { get; set; } = new List<AFFECTATION_VEHICULE>();

    public virtual ICollection<DEPENSE_INTERVENTION> DEPENSE_INTERVENTIONs { get; set; } = new List<DEPENSE_INTERVENTION>();

    public virtual ICollection<DEPENSE_VEHICULE> DEPENSE_VEHICULEs { get; set; } = new List<DEPENSE_VEHICULE>();

    public virtual ICollection<FACTURE> FACTUREs { get; set; } = new List<FACTURE>();

    public virtual UTILISATEUR ID_CREATEURNavigation { get; set; } = null!;

    public virtual LOCATAIRE ID_LOCATAIRENavigation { get; set; } = null!;

    public virtual UTILISATEUR? ID_VALIDATEURNavigation { get; set; }

    public virtual ICollection<MOUVEMENT_STOCK> MOUVEMENT_STOCKs { get; set; } = new List<MOUVEMENT_STOCK>();

    public virtual ICollection<PHOTO_INTERVENTION> PHOTO_INTERVENTIONs { get; set; } = new List<PHOTO_INTERVENTION>();

    public virtual REF_STATUT_INTERVENTION STATUTNavigation { get; set; } = null!;

    public virtual REF_TYPE_INTERVENTION TYPENavigation { get; set; } = null!;
}
