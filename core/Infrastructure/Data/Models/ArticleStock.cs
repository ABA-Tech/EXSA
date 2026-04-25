using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class ArticleStock
{
    public Guid IdArticle { get; set; }

    public Guid IdLocataire { get; set; }

    public string Nom { get; set; } = null!;

    public string? Reference { get; set; }

    public string Unite { get; set; } = null!;

    public decimal StockActuel { get; set; }

    public decimal StockMinimum { get; set; }

    public decimal? PrixUnitaireXaf { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public virtual Locataire IdLocataireNavigation { get; set; } = null!;

    public virtual ICollection<MouvementStock> MouvementStocks { get; set; } = new List<MouvementStock>();
}
