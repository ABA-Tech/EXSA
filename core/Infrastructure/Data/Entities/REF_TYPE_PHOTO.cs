using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_TYPE_PHOTO
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public virtual ICollection<PHOTO_INTERVENTION> PHOTO_INTERVENTIONs { get; set; } = new List<PHOTO_INTERVENTION>();
}
