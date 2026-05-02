using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class EMPLOYE
{
    public Guid ID_EMPLOYE { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public Guid? ID_UTILISATEUR { get; set; }

    public string NUMERO_EMPLOYE { get; set; } = null!;

    public decimal SALAIRE_BASE_XAF { get; set; }

    public string TYPE_CONTRAT { get; set; } = null!;

    public DateTime DATE_EMBAUCHE { get; set; }

    public string? NUMERO_CNPS { get; set; }

    public bool? EST_ACTIF { get; set; }

    public DateTime DATE_CREATION { get; set; }

    public DateTime? DATE_MODIFICATION { get; set; }

    public virtual ICollection<DEPENSE_INTERVENTION> DEPENSE_INTERVENTIONs { get; set; } = new List<DEPENSE_INTERVENTION>();

    public virtual LOCATAIRE ID_LOCATAIRENavigation { get; set; } = null!;

    public virtual UTILISATEUR? ID_UTILISATEURNavigation { get; set; }

    public virtual REF_TYPE_CONTRAT TYPE_CONTRATNavigation { get; set; } = null!;
}
