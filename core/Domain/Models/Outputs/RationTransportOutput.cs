namespace Domain.Models.Outputs
{
    public class RationTransportOutput
    {
        public DateTime JOUR { get; set; }
        public Guid ID_EMPLOYE { get; set; }
        public string NOM_COMPLET { get; set; } = string.Empty;
        public string NUMERO_EMPLOYE { get; set; } = string.Empty;
        public bool EST_PRINCIPAL { get; set; }

        // Valeurs brutes — nullable intentionnellement
        public decimal? MONTANT_RATION { get; set; }
        public decimal? MONTANT_TRANSPORT { get; set; }
        public Guid? ID_DEPENSE_RATION { get; set; }
        public Guid? ID_DEPENSE_TRANSPORT { get; set; }
        public Guid? ID_DEPENSE { get; set; }
        public decimal TOTAL_CASE { get; set; }

        // Sous-totaux précalculés par la SP (fenêtrage SQL)
        public decimal SOUS_TOTAL_RATIONS_JOUR { get; set; }
        public decimal SOUS_TOTAL_TRANSPORT_JOUR { get; set; }
        public decimal SOUS_TOTAL_JOUR { get; set; }

        public decimal SOUS_TOTAL_RATIONS_TECH { get; set; }
        public decimal SOUS_TOTAL_TRANSPORT_TECH { get; set; }
        public decimal SOUS_TOTAL_TECH { get; set; }

        public decimal GRAND_TOTAL_INTERVENTION { get; set; }

        public int NB_JOURS_AVEC_RATION { get; set; }
        public int NB_JOURS_AVEC_TRANSPORT { get; set; }
    }
}
