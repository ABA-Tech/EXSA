using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class PHOTO_INTERVENTION
{
    public Guid ID_PHOTO { get; set; }

    public Guid ID_INTERVENTION { get; set; }

    public string URL_BLOB { get; set; } = null!;

    public string TYPE_PHOTO { get; set; } = null!;

    public DateTime DATE_PRISE { get; set; }

    public Guid ID_UPLOADEUR { get; set; }

    public decimal? LATITUDE { get; set; }

    public decimal? LONGITUDE { get; set; }

    public virtual INTERVENTION ID_INTERVENTIONNavigation { get; set; } = null!;

    public virtual UTILISATEUR ID_UPLOADEURNavigation { get; set; } = null!;

    public virtual REF_TYPE_PHOTO TYPE_PHOTONavigation { get; set; } = null!;
}
