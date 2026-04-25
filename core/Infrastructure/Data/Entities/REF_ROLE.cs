using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_ROLE
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public virtual ICollection<UTILISATEUR> UTILISATEURs { get; set; } = new List<UTILISATEUR>();
}
