using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class DEPENSE_INTERVENTION
{
    public Guid ID_DEPENSE { get; set; }

    public Guid ID_INTERVENTION { get; set; }

    public Guid? ID_EMPLOYE { get; set; }

    public Guid? ID_SAISIE_PAR { get; set; }

    public string REFERENCE { get; set; } = null!;

    public string TYPE_DEPENSE { get; set; } = null!;

    public decimal MONTANT_XAF { get; set; }

    public string? NOTE { get; set; }

    public DateTime? DATE_DEPENSE { get; set; }

    public DateTime DATE_CREATION { get; set; }

    public virtual EMPLOYE? ID_EMPLOYENavigation { get; set; }

    public virtual INTERVENTION ID_INTERVENTIONNavigation { get; set; } = null!;

    public virtual UTILISATEUR? ID_SAISIE_PARNavigation { get; set; }

    public virtual REF_TYPE_DEPENSE_INTERVENTION TYPE_DEPENSENavigation { get; set; } = null!;
}
