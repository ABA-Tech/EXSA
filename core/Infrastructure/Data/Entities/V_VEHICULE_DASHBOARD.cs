using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class V_VEHICULE_DASHBOARD
{
    public Guid ID_VEHICULE { get; set; }

    public string IMMATRICULATION { get; set; } = null!;

    public string DESIGNATION { get; set; } = null!;

    public string TYPE_VEHICULE { get; set; } = null!;

    public string STATUT { get; set; } = null!;

    public string STATUT_LIBELLE { get; set; } = null!;

    public string? COULEUR_HEX { get; set; }

    public int KILOMETRAGE_ACTUEL { get; set; }

    public DateTime? ASSURANCE_EXPIRATION { get; set; }

    public DateTime? VIGNETTE_EXPIRATION { get; set; }

    public DateTime? VISITE_TECHNIQUE_EXPIRATION { get; set; }

    public decimal TOTAL_DEPENSES_XAF { get; set; }

    public decimal DEPENSES_EN_MISSION_XAF { get; set; }

    public decimal DEPENSES_HORS_MISSION_XAF { get; set; }

    public int KM_TOTAL_MISSIONS { get; set; }

    public int NB_AFFECTATIONS { get; set; }

    public int ENTRETIENS_EN_RETARD { get; set; }

    public int ENTRETIENS_PLANIFIES { get; set; }

    public int A_DOCUMENT_EXPIRE { get; set; }

    public int A_DOCUMENT_EXPIRE_BIENTOT { get; set; }
}
