using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class MOUVEMENT_STOCK
{
    public Guid ID_MOUVEMENT { get; set; }

    public Guid ID_ARTICLE { get; set; }

    public Guid? ID_INTERVENTION { get; set; }

    public string TYPE_MOUVEMENT { get; set; } = null!;

    public decimal QUANTITE { get; set; }

    public Guid ID_OPERATEUR { get; set; }

    public DateTime DATE_MOUVEMENT { get; set; }

    public virtual ARTICLE_STOCK ID_ARTICLENavigation { get; set; } = null!;

    public virtual INTERVENTION? ID_INTERVENTIONNavigation { get; set; }

    public virtual UTILISATEUR ID_OPERATEURNavigation { get; set; } = null!;

    public virtual REF_TYPE_MOUVEMENT TYPE_MOUVEMENTNavigation { get; set; } = null!;
}
