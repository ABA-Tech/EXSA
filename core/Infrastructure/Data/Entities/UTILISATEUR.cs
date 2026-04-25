using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class UTILISATEUR
{
    public Guid ID_UTILISATEUR { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public string NOM_COMPLET { get; set; } = null!;

    public string TELEPHONE { get; set; } = null!;

    public string? EMAIL { get; set; }

    public string MOT_DE_PASSE_HASH { get; set; } = null!;

    public string ROLE { get; set; } = null!;

    public string? TOKEN_FCM { get; set; }

    public bool? EST_ACTIF { get; set; }

    public DateTime? DERNIERE_CONNEXION { get; set; }

    public DateTime DATE_CREATION { get; set; }

    public DateTime? DATE_MODIFICATION { get; set; }

    public bool EST_SUPPRIME { get; set; }

    public virtual ICollection<AFFECTATION_INTERVENTION> AFFECTATION_INTERVENTIONs { get; set; } = new List<AFFECTATION_INTERVENTION>();

    public virtual ICollection<EMPLOYE> EMPLOYEs { get; set; } = new List<EMPLOYE>();

    public virtual LOCATAIRE ID_LOCATAIRENavigation { get; set; } = null!;

    public virtual ICollection<INTERVENTION> INTERVENTIONID_CREATEURNavigations { get; set; } = new List<INTERVENTION>();

    public virtual ICollection<INTERVENTION> INTERVENTIONID_VALIDATEURNavigations { get; set; } = new List<INTERVENTION>();

    public virtual ICollection<MOUVEMENT_STOCK> MOUVEMENT_STOCKs { get; set; } = new List<MOUVEMENT_STOCK>();

    public virtual ICollection<PHOTO_INTERVENTION> PHOTO_INTERVENTIONs { get; set; } = new List<PHOTO_INTERVENTION>();

    public virtual ICollection<POSITION_GP> POSITION_GPs { get; set; } = new List<POSITION_GP>();

    public virtual REF_ROLE ROLENavigation { get; set; } = null!;
}
