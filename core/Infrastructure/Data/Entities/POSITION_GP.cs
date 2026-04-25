using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class POSITION_GP
{
    public Guid ID_POSITION { get; set; }

    public Guid ID_UTILISATEUR { get; set; }

    public decimal LATITUDE { get; set; }

    public decimal LONGITUDE { get; set; }

    public decimal? PRECISION_METRES { get; set; }

    public DateTime HORODATAGE { get; set; }

    public virtual UTILISATEUR ID_UTILISATEURNavigation { get; set; } = null!;
}
