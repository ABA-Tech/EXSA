using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class PositionGp
{
    public Guid IdPosition { get; set; }

    public Guid IdUtilisateur { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public decimal? PrecisionMetres { get; set; }

    public DateTime Horodatage { get; set; }

    public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;
}
