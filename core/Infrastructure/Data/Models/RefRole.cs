using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class RefRole
{
    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
