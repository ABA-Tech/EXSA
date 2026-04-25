using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class ARTICLE_STOCK
{
    public Guid ID_ARTICLE { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public string NOM { get; set; } = null!;

    public string? REFERENCE { get; set; }

    public string UNITE { get; set; } = null!;

    public decimal STOCK_ACTUEL { get; set; }

    public decimal STOCK_MINIMUM { get; set; }

    public decimal? PRIX_UNITAIRE_XAF { get; set; }

    public DateTime DATE_CREATION { get; set; }

    public DateTime? DATE_MODIFICATION { get; set; }

    public virtual LOCATAIRE ID_LOCATAIRENavigation { get; set; } = null!;

    public virtual ICollection<MOUVEMENT_STOCK> MOUVEMENT_STOCKs { get; set; } = new List<MOUVEMENT_STOCK>();
}
