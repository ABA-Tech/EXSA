using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class VueArticlesAlerteStock
{
    public Guid IdArticle { get; set; }

    public Guid IdLocataire { get; set; }

    public string NomLocataire { get; set; } = null!;

    public string NomArticle { get; set; } = null!;

    public string? Reference { get; set; }

    public string Unite { get; set; } = null!;

    public decimal StockActuel { get; set; }

    public decimal StockMinimum { get; set; }

    public decimal? PrixUnitaireXaf { get; set; }
}
