using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class VUE_ARTICLES_ALERTE_STOCK
{
    public Guid ID_ARTICLE { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public string NOM_LOCATAIRE { get; set; } = null!;

    public string NOM_ARTICLE { get; set; } = null!;

    public string? REFERENCE { get; set; }

    public string UNITE { get; set; } = null!;

    public decimal STOCK_ACTUEL { get; set; }

    public decimal STOCK_MINIMUM { get; set; }

    public decimal? PRIX_UNITAIRE_XAF { get; set; }
}
