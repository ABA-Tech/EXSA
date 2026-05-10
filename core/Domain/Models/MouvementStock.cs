using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class MouvementStock
{
    public Guid IdMouvement { get; set; }

    public Guid IdArticle { get; set; }

    public Guid? IdIntervention { get; set; }

    public string TypeMouvement { get; set; } = null!;

    public decimal Quantite { get; set; }

    public Guid IdOperateur { get; set; }

    public DateTime DateMouvement { get; set; }

    public virtual ArticleStock? IdArticleNavigation { get; set; } = null!;

    public virtual Intervention? IdIntervention { get; set; }


    public virtual RefTypeMouvement? TypeMouvement { get; set; } = null!;
}
