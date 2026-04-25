using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class LOCATAIRE
{
    public Guid ID_LOCATAIRE { get; set; }

    public string NOM { get; set; } = null!;

    public string SLUG { get; set; } = null!;

    public string TYPE_PLAN { get; set; } = null!;

    public string MODULES_ACTIFS { get; set; } = null!;

    public string CODE_PAYS { get; set; } = null!;

    public string PREFIXE_TELEPHONE { get; set; } = null!;

    public bool? EST_ACTIF { get; set; }

    public DateTime DATE_CREATION { get; set; }

    public DateTime? DATE_MODIFICATION { get; set; }

    public virtual ICollection<ARTICLE_STOCK> ARTICLE_STOCKs { get; set; } = new List<ARTICLE_STOCK>();

    public virtual ICollection<EMPLOYE> EMPLOYEs { get; set; } = new List<EMPLOYE>();

    public virtual ICollection<FACTURE> FACTUREs { get; set; } = new List<FACTURE>();

    public virtual ICollection<INTERVENTION> INTERVENTIONs { get; set; } = new List<INTERVENTION>();

    public virtual REF_TYPE_PLAN TYPE_PLANNavigation { get; set; } = null!;

    public virtual ICollection<UTILISATEUR> UTILISATEURs { get; set; } = new List<UTILISATEUR>();
}
