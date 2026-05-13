using System;
using System.Collections.Generic;
using Domain.Models.Outputs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Entities;

public partial class ExsaDbContext : DbContext
{
    public ExsaDbContext()
    {
    }

    public ExsaDbContext(DbContextOptions<ExsaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AFFECTATION_INTERVENTION> AFFECTATION_INTERVENTIONs { get; set; }

    public virtual DbSet<AFFECTATION_VEHICULE> AFFECTATION_VEHICULEs { get; set; }

    public virtual DbSet<ARTICLE_STOCK> ARTICLE_STOCKs { get; set; }

    public virtual DbSet<DEPENSE_INTERVENTION> DEPENSE_INTERVENTIONs { get; set; }

    public virtual DbSet<DEPENSE_VEHICULE> DEPENSE_VEHICULEs { get; set; }

    public virtual DbSet<EMPLOYE> EMPLOYEs { get; set; }

    public virtual DbSet<ENTRETIEN_VEHICULE> ENTRETIEN_VEHICULEs { get; set; }

    public virtual DbSet<FACTURE> FACTUREs { get; set; }

    public virtual DbSet<INTERVENTION> INTERVENTIONs { get; set; }

    public virtual DbSet<LIGNE_FACTURE> LIGNE_FACTUREs { get; set; }

    public virtual DbSet<LOCATAIRE> LOCATAIREs { get; set; }

    public virtual DbSet<MOUVEMENT_STOCK> MOUVEMENT_STOCKs { get; set; }

    public virtual DbSet<PHOTO_INTERVENTION> PHOTO_INTERVENTIONs { get; set; }

    public virtual DbSet<POSITION_GP> POSITION_GPs { get; set; }

    public virtual DbSet<REF_CATEGORIE_DEPENSE> REF_CATEGORIE_DEPENSEs { get; set; }

    public virtual DbSet<REF_ROLE> REF_ROLEs { get; set; }

    public virtual DbSet<REF_STATUT_FACTURE> REF_STATUT_FACTUREs { get; set; }

    public virtual DbSet<REF_STATUT_INTERVENTION> REF_STATUT_INTERVENTIONs { get; set; }

    public virtual DbSet<REF_STATUT_VEHICULE> REF_STATUT_VEHICULEs { get; set; }

    public virtual DbSet<REF_TYPE_CONTRAT> REF_TYPE_CONTRATs { get; set; }

    public virtual DbSet<REF_TYPE_DEPENSE_INTERVENTION> REF_TYPE_DEPENSE_INTERVENTIONs { get; set; }

    public virtual DbSet<REF_TYPE_DEPENSE_VEHICULE> REF_TYPE_DEPENSE_VEHICULEs { get; set; }

    public virtual DbSet<REF_TYPE_ENTRETIEN> REF_TYPE_ENTRETIENs { get; set; }

    public virtual DbSet<REF_TYPE_INTERVENTION> REF_TYPE_INTERVENTIONs { get; set; }

    public virtual DbSet<REF_TYPE_MOUVEMENT> REF_TYPE_MOUVEMENTs { get; set; }

    public virtual DbSet<REF_TYPE_PHOTO> REF_TYPE_PHOTOs { get; set; }

    public virtual DbSet<REF_TYPE_PLAN> REF_TYPE_PLANs { get; set; }

    public virtual DbSet<REF_TYPE_VEHICULE> REF_TYPE_VEHICULEs { get; set; }

    public virtual DbSet<UTILISATEUR> UTILISATEURs { get; set; }

    public virtual DbSet<VEHICULE> VEHICULEs { get; set; }

    public virtual DbSet<VUE_ARTICLES_ALERTE_STOCK> VUE_ARTICLES_ALERTE_STOCKs { get; set; }

    public virtual DbSet<VUE_FACTURES_EN_RETARD> VUE_FACTURES_EN_RETARDs { get; set; }

    public virtual DbSet<VUE_INTERVENTION_TECHNICIEN> VUE_INTERVENTION_TECHNICIENs { get; set; }

    public virtual DbSet<V_ALERTES_VEHICULE> V_ALERTES_VEHICULEs { get; set; }

    public virtual DbSet<V_DEPENSES_PAR_VEHICULE> V_DEPENSES_PAR_VEHICULEs { get; set; }

    public virtual DbSet<V_VEHICULE_DASHBOARD> V_VEHICULE_DASHBOARDs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AFFECTATION_INTERVENTION>(entity =>
        {
            entity.HasKey(e => e.ID_AFFECTATION);

            entity.ToTable("AFFECTATION_INTERVENTION");

            entity.HasIndex(e => new { e.ID_INTERVENTION, e.ID_TECHNICIEN }, "UQ_AFFECTATION_INTERV_TECH").IsUnique();

            entity.Property(e => e.ID_AFFECTATION).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.DATE_AFFECTATION).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.ID_INTERVENTIONNavigation).WithMany(p => p.AFFECTATION_INTERVENTIONs)
                .HasForeignKey(d => d.ID_INTERVENTION)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AFFECTATION_INTERVENTION");

            entity.HasOne(d => d.ID_TECHNICIENNavigation).WithMany(p => p.AFFECTATION_INTERVENTIONs)
                .HasForeignKey(d => d.ID_TECHNICIEN)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AFFECTATION_TECHNICIEN");
        });

        modelBuilder.Entity<AFFECTATION_VEHICULE>(entity =>
        {
            entity.HasKey(e => e.ID_AFFECTATION_VEHICULE);

            entity.ToTable("AFFECTATION_VEHICULE", tb =>
                {
                    tb.HasTrigger("TR_AFFECTATION_VEHICULE_UpdateKm");
                    tb.HasTrigger("TR_AFFECTATION_VEHICULE_UpdateStatut");
                });

            entity.HasIndex(e => e.ID_INTERVENTION, "IX_AFFVEH_INTERVENTION");

            entity.HasIndex(e => new { e.ID_VEHICULE, e.DATE_FIN }, "IX_AFFVEH_VEHICULE_ACTIVE").HasFilter("([DATE_FIN] IS NULL)");

            entity.Property(e => e.ID_AFFECTATION_VEHICULE).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.DATE_CREATION).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DATE_DEBUT).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DISTANCE_KM).HasComputedColumnSql("(case when [KILOMETRAGE_ARRIVEE] IS NOT NULL then [KILOMETRAGE_ARRIVEE]-[KILOMETRAGE_DEPART]  end)", true);

            entity.HasOne(d => d.ID_CONDUCTEURNavigation).WithMany(p => p.AFFECTATION_VEHICULEs)
                .HasForeignKey(d => d.ID_CONDUCTEUR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AFFVEH_CONDUCTEUR");

            entity.HasOne(d => d.ID_INTERVENTIONNavigation).WithMany(p => p.AFFECTATION_VEHICULEs)
                .HasForeignKey(d => d.ID_INTERVENTION)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AFFVEH_INTERVENTION");

            entity.HasOne(d => d.ID_VEHICULENavigation).WithMany(p => p.AFFECTATION_VEHICULEs)
                .HasForeignKey(d => d.ID_VEHICULE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AFFVEH_VEHICULE");
        });

        modelBuilder.Entity<ARTICLE_STOCK>(entity =>
        {
            entity.HasKey(e => e.ID_ARTICLE);

            entity.ToTable("ARTICLE_STOCK", tb => tb.HasTrigger("TRG_ARTICLE_STOCK_UPDATE"));

            entity.Property(e => e.ID_ARTICLE).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.DATE_CREATION).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.NOM).HasMaxLength(200);
            entity.Property(e => e.PRIX_UNITAIRE_XAF).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.REFERENCE).HasMaxLength(100);
            entity.Property(e => e.STOCK_ACTUEL).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.STOCK_MINIMUM).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.UNITE).HasMaxLength(30);

            entity.HasOne(d => d.ID_LOCATAIRENavigation).WithMany(p => p.ARTICLE_STOCKs)
                .HasForeignKey(d => d.ID_LOCATAIRE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ARTICLE_LOCATAIRE");
        });

        modelBuilder.Entity<DEPENSE_INTERVENTION>(entity =>
        {
            entity.HasKey(e => e.ID_DEPENSE).HasName("PK__DEPENSE___AE892F857DDEE58E");

            entity.ToTable("DEPENSE_INTERVENTION");

            entity.Property(e => e.ID_DEPENSE).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.DATE_CREATION).HasColumnType("datetime");
            entity.Property(e => e.DATE_DEPENSE).HasColumnType("datetime");
            entity.Property(e => e.MONTANT_XAF).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.NOTE)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.REFERENCE)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TYPE_DEPENSE).HasMaxLength(20);

            entity.HasOne(d => d.ID_EMPLOYENavigation).WithMany(p => p.DEPENSE_INTERVENTIONs)
                .HasForeignKey(d => d.ID_EMPLOYE)
                .HasConstraintName("FK_DEPENSE_EMPLOYE");

            entity.HasOne(d => d.ID_INTERVENTIONNavigation).WithMany(p => p.DEPENSE_INTERVENTIONs)
                .HasForeignKey(d => d.ID_INTERVENTION)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEPENSE_INTERVENTION");

            entity.HasOne(d => d.ID_SAISIE_PARNavigation).WithMany(p => p.DEPENSE_INTERVENTIONs)
                .HasForeignKey(d => d.ID_SAISIE_PAR)
                .HasConstraintName("FK_DEPENSE_UTILISATEUR");

            entity.HasOne(d => d.TYPE_DEPENSENavigation).WithMany(p => p.DEPENSE_INTERVENTIONs)
                .HasForeignKey(d => d.TYPE_DEPENSE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEPENSE_TYPE_DEPENSE");
        });

        modelBuilder.Entity<DEPENSE_VEHICULE>(entity =>
        {
            entity.HasKey(e => e.ID_DEPENSE);

            entity.ToTable("DEPENSE_VEHICULE");

            entity.HasIndex(e => new { e.ID_LOCATAIRE, e.DATE_DEPENSE }, "IX_DEPVEH_HORS_MISSION")
                .IsDescending(false, true)
                .HasFilter("([ID_INTERVENTION] IS NULL)");

            entity.HasIndex(e => e.ID_INTERVENTION, "IX_DEPVEH_INTERVENTION").HasFilter("([ID_INTERVENTION] IS NOT NULL)");

            entity.HasIndex(e => new { e.ID_LOCATAIRE, e.DATE_DEPENSE }, "IX_DEPVEH_LOCATAIRE_DATE");

            entity.HasIndex(e => new { e.ID_VEHICULE, e.DATE_DEPENSE }, "IX_DEPVEH_VEHICULE_DATE").IsDescending(false, true);

            entity.Property(e => e.ID_DEPENSE).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.DATE_CREATION).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DATE_DEPENSE).HasColumnType("date");
            entity.Property(e => e.DESCRIPTION).HasMaxLength(500);
            entity.Property(e => e.MONTANT_XAF).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TYPE_DEPENSE).HasMaxLength(30);
            entity.Property(e => e.URL_JUSTIFICATIF).HasMaxLength(500);

            entity.HasOne(d => d.ID_INTERVENTIONNavigation).WithMany(p => p.DEPENSE_VEHICULEs)
                .HasForeignKey(d => d.ID_INTERVENTION)
                .HasConstraintName("FK_DEPVEH_INTERVENTION");

            entity.HasOne(d => d.ID_LOCATAIRENavigation).WithMany(p => p.DEPENSE_VEHICULEs)
                .HasForeignKey(d => d.ID_LOCATAIRE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEPVEH_LOCATAIRE");

            entity.HasOne(d => d.ID_SAISIE_PARNavigation).WithMany(p => p.DEPENSE_VEHICULEs)
                .HasForeignKey(d => d.ID_SAISIE_PAR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEPVEH_SAISIE");

            entity.HasOne(d => d.ID_VEHICULENavigation).WithMany(p => p.DEPENSE_VEHICULEs)
                .HasForeignKey(d => d.ID_VEHICULE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEPVEH_VEHICULE");

            entity.HasOne(d => d.TYPE_DEPENSENavigation).WithMany(p => p.DEPENSE_VEHICULEs)
                .HasForeignKey(d => d.TYPE_DEPENSE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEPVEH_TYPE");
        });

        modelBuilder.Entity<EMPLOYE>(entity =>
        {
            entity.HasKey(e => e.ID_EMPLOYE);

            entity.ToTable("EMPLOYE", tb => tb.HasTrigger("TRG_EMPLOYE_UPDATE"));

            entity.HasIndex(e => new { e.ID_LOCATAIRE, e.NUMERO_EMPLOYE }, "UQ_EMPLOYE_NUM_TENANT").IsUnique();

            entity.Property(e => e.ID_EMPLOYE).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.DATE_CREATION).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DATE_EMBAUCHE).HasColumnType("date");
            entity.Property(e => e.EST_ACTIF)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.NUMERO_CNPS).HasMaxLength(50);
            entity.Property(e => e.NUMERO_EMPLOYE).HasMaxLength(30);
            entity.Property(e => e.SALAIRE_BASE_XAF).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.TYPE_CONTRAT).HasMaxLength(20);

            entity.HasOne(d => d.ID_LOCATAIRENavigation).WithMany(p => p.EMPLOYEs)
                .HasForeignKey(d => d.ID_LOCATAIRE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLOYE_LOCATAIRE");

            entity.HasOne(d => d.ID_UTILISATEURNavigation).WithMany(p => p.EMPLOYEs)
                .HasForeignKey(d => d.ID_UTILISATEUR)
                .HasConstraintName("FK_EMPLOYE_UTILISATEUR");

            entity.HasOne(d => d.TYPE_CONTRATNavigation).WithMany(p => p.EMPLOYEs)
                .HasForeignKey(d => d.TYPE_CONTRAT)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLOYE_CONTRAT");
        });

        modelBuilder.Entity<ENTRETIEN_VEHICULE>(entity =>
        {
            entity.HasKey(e => e.ID_ENTRETIEN);

            entity.ToTable("ENTRETIEN_VEHICULE", tb => tb.HasTrigger("TR_ENTRETIEN_VEHICULE_UpdateStatut"));

            entity.HasIndex(e => new { e.ID_LOCATAIRE, e.STATUT, e.DATE_PREVUE }, "IX_ENTVEH_STATUT_DATE").HasFilter("([STATUT] IN ('PLANIFIE', 'EN_COURS', 'EN_RETARD'))");

            entity.Property(e => e.ID_ENTRETIEN).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.COUT_XAF).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DATE_CREATION).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DATE_PREVUE).HasColumnType("date");
            entity.Property(e => e.DATE_REALISE).HasColumnType("date");
            entity.Property(e => e.PRESTATAIRE).HasMaxLength(200);
            entity.Property(e => e.STATUT)
                .HasMaxLength(20)
                .HasDefaultValueSql("('PLANIFIE')");
            entity.Property(e => e.TYPE_ENTRETIEN).HasMaxLength(30);

            entity.HasOne(d => d.ID_CREATEURNavigation).WithMany(p => p.ENTRETIEN_VEHICULEs)
                .HasForeignKey(d => d.ID_CREATEUR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENTVEH_CREATEUR");

            entity.HasOne(d => d.ID_DEPENSENavigation).WithMany(p => p.ENTRETIEN_VEHICULEs)
                .HasForeignKey(d => d.ID_DEPENSE)
                .HasConstraintName("FK_ENTVEH_DEPENSE");

            entity.HasOne(d => d.ID_LOCATAIRENavigation).WithMany(p => p.ENTRETIEN_VEHICULEs)
                .HasForeignKey(d => d.ID_LOCATAIRE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENTVEH_LOCATAIRE");

            entity.HasOne(d => d.ID_VEHICULENavigation).WithMany(p => p.ENTRETIEN_VEHICULEs)
                .HasForeignKey(d => d.ID_VEHICULE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENTVEH_VEHICULE");

            entity.HasOne(d => d.TYPE_ENTRETIENNavigation).WithMany(p => p.ENTRETIEN_VEHICULEs)
                .HasForeignKey(d => d.TYPE_ENTRETIEN)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENTVEH_TYPE");
        });

        modelBuilder.Entity<FACTURE>(entity =>
        {
            entity.HasKey(e => e.ID_FACTURE);

            entity.ToTable("FACTURE", tb => tb.HasTrigger("TRG_FACTURE_UPDATE"));

            entity.HasIndex(e => new { e.ID_LOCATAIRE, e.REFERENCE }, "UQ_FACTURE_REF_TENANT").IsUnique();

            entity.Property(e => e.ID_FACTURE).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.DATE_CREATION).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DATE_ECHEANCE).HasColumnType("date");
            entity.Property(e => e.NOM_CLIENT).HasMaxLength(200);
            entity.Property(e => e.REFERENCE).HasMaxLength(30);
            entity.Property(e => e.SOUS_TOTAL_XAF).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.STATUT)
                .HasMaxLength(30)
                .HasDefaultValueSql("('BROUILLON')");
            entity.Property(e => e.TAUX_TVA)
                .HasDefaultValueSql("((19.25))")
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TOTAL_XAF).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.URL_PDF).HasMaxLength(500);

            entity.HasOne(d => d.ID_INTERVENTIONNavigation).WithMany(p => p.FACTUREs)
                .HasForeignKey(d => d.ID_INTERVENTION)
                .HasConstraintName("FK_FACTURE_INTERVENTION");

            entity.HasOne(d => d.ID_LOCATAIRENavigation).WithMany(p => p.FACTUREs)
                .HasForeignKey(d => d.ID_LOCATAIRE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FACTURE_LOCATAIRE");

            entity.HasOne(d => d.STATUTNavigation).WithMany(p => p.FACTUREs)
                .HasForeignKey(d => d.STATUT)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FACTURE_STATUT");
        });

        modelBuilder.Entity<INTERVENTION>(entity =>
        {
            entity.HasKey(e => e.ID_INTERVENTION);

            entity.ToTable("INTERVENTION", tb => tb.HasTrigger("TRG_INTERVENTION_UPDATE"));

            entity.HasIndex(e => new { e.ID_LOCATAIRE, e.REFERENCE }, "UQ_INTERVENTION_REF_TENANT").IsUnique();

            entity.Property(e => e.ID_INTERVENTION).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ADRESSE).HasMaxLength(500);
            entity.Property(e => e.DATE_CREATION).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ID_LOCAL).HasMaxLength(50);
            entity.Property(e => e.LATITUDE).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.LONGITUDE).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.NOM_CLIENT).HasMaxLength(200);
            entity.Property(e => e.PRIORITE).HasDefaultValueSql("((2))");
            entity.Property(e => e.REFERENCE).HasMaxLength(30);
            entity.Property(e => e.STATUT)
                .HasMaxLength(20)
                .HasDefaultValueSql("('CREEE')");
            entity.Property(e => e.TITRE).HasMaxLength(300);
            entity.Property(e => e.TYPE).HasMaxLength(20);
            entity.Property(e => e.URL_SIGNATURE).HasMaxLength(500);

            entity.HasOne(d => d.ID_CREATEURNavigation).WithMany(p => p.INTERVENTIONID_CREATEURNavigations)
                .HasForeignKey(d => d.ID_CREATEUR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INTERVENTION_CREATEUR");

            entity.HasOne(d => d.ID_LOCATAIRENavigation).WithMany(p => p.INTERVENTIONs)
                .HasForeignKey(d => d.ID_LOCATAIRE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INTERVENTION_LOCATAIRE");

            entity.HasOne(d => d.ID_VALIDATEURNavigation).WithMany(p => p.INTERVENTIONID_VALIDATEURNavigations)
                .HasForeignKey(d => d.ID_VALIDATEUR)
                .HasConstraintName("FK_INTERVENTION_VALIDATEUR");

            entity.HasOne(d => d.STATUTNavigation).WithMany(p => p.INTERVENTIONs)
                .HasForeignKey(d => d.STATUT)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INTERVENTION_STATUT");

            entity.HasOne(d => d.TYPENavigation).WithMany(p => p.INTERVENTIONs)
                .HasForeignKey(d => d.TYPE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INTERVENTION_TYPE");
        });

        modelBuilder.Entity<LIGNE_FACTURE>(entity =>
        {
            entity.HasKey(e => e.ID_LIGNE);

            entity.ToTable("LIGNE_FACTURE");

            entity.Property(e => e.ID_LIGNE).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.DESCRIPTION).HasMaxLength(500);
            entity.Property(e => e.PRIX_UNITAIRE).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.QUANTITE).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TOTAL_XAF).HasColumnType("decimal(15, 2)");

            entity.HasOne(d => d.ID_FACTURENavigation).WithMany(p => p.LIGNE_FACTUREs)
                .HasForeignKey(d => d.ID_FACTURE)
                .HasConstraintName("FK_LIGNE_FACTURE");
        });

        modelBuilder.Entity<LOCATAIRE>(entity =>
        {
            entity.HasKey(e => e.ID_LOCATAIRE);

            entity.ToTable("LOCATAIRE", tb => tb.HasTrigger("TRG_LOCATAIRE_UPDATE"));

            entity.HasIndex(e => e.SLUG, "UQ_LOCATAIRE_SLUG").IsUnique();

            entity.Property(e => e.ID_LOCATAIRE).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.CODE_PAYS)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("('CM')")
                .IsFixedLength();
            entity.Property(e => e.DATE_CREATION).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.EST_ACTIF)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.MODULES_ACTIFS)
                .HasMaxLength(500)
                .HasDefaultValueSql("('CORE')");
            entity.Property(e => e.NOM).HasMaxLength(200);
            entity.Property(e => e.PREFIXE_TELEPHONE)
                .HasMaxLength(5)
                .HasDefaultValueSql("('+237')");
            entity.Property(e => e.SLUG).HasMaxLength(100);
            entity.Property(e => e.TYPE_PLAN)
                .HasMaxLength(20)
                .HasDefaultValueSql("('STARTER')");

            entity.HasOne(d => d.TYPE_PLANNavigation).WithMany(p => p.LOCATAIREs)
                .HasForeignKey(d => d.TYPE_PLAN)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOCATAIRE_PLAN");
        });

        modelBuilder.Entity<MOUVEMENT_STOCK>(entity =>
        {
            entity.HasKey(e => e.ID_MOUVEMENT);

            entity.ToTable("MOUVEMENT_STOCK", tb => tb.HasTrigger("TRG_MOUVEMENT_STOCK_UPDATE_STOCK"));

            entity.HasIndex(e => new { e.ID_ARTICLE, e.DATE_MOUVEMENT }, "IX_MOUVEMENT_ARTICLE_DATE").IsDescending(false, true);

            entity.Property(e => e.ID_MOUVEMENT).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.DATE_MOUVEMENT).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.QUANTITE).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.TYPE_MOUVEMENT).HasMaxLength(10);

            entity.HasOne(d => d.ID_ARTICLENavigation).WithMany(p => p.MOUVEMENT_STOCKs)
                .HasForeignKey(d => d.ID_ARTICLE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MOUVEMENT_ARTICLE");

            entity.HasOne(d => d.ID_INTERVENTIONNavigation).WithMany(p => p.MOUVEMENT_STOCKs)
                .HasForeignKey(d => d.ID_INTERVENTION)
                .HasConstraintName("FK_MOUVEMENT_INTERVENTION");

            entity.HasOne(d => d.ID_OPERATEURNavigation).WithMany(p => p.MOUVEMENT_STOCKs)
                .HasForeignKey(d => d.ID_OPERATEUR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MOUVEMENT_OPERATEUR");

            entity.HasOne(d => d.TYPE_MOUVEMENTNavigation).WithMany(p => p.MOUVEMENT_STOCKs)
                .HasForeignKey(d => d.TYPE_MOUVEMENT)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MOUVEMENT_TYPE");
        });

        modelBuilder.Entity<PHOTO_INTERVENTION>(entity =>
        {
            entity.HasKey(e => e.ID_PHOTO);

            entity.ToTable("PHOTO_INTERVENTION");

            entity.Property(e => e.ID_PHOTO).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.COMMENTAIRE)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.LATITUDE).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.LONGITUDE).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.TYPE_PHOTO)
                .HasMaxLength(20)
                .HasDefaultValueSql("('AUTRE')");
            entity.Property(e => e.URL_BLOB).HasMaxLength(1000);

            entity.HasOne(d => d.ID_INTERVENTIONNavigation).WithMany(p => p.PHOTO_INTERVENTIONs)
                .HasForeignKey(d => d.ID_INTERVENTION)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHOTO_INTERVENTION");

            entity.HasOne(d => d.ID_UPLOADEURNavigation).WithMany(p => p.PHOTO_INTERVENTIONs)
                .HasForeignKey(d => d.ID_UPLOADEUR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHOTO_UPLOADEUR");

            entity.HasOne(d => d.TYPE_PHOTONavigation).WithMany(p => p.PHOTO_INTERVENTIONs)
                .HasForeignKey(d => d.TYPE_PHOTO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHOTO_TYPE");
        });

        modelBuilder.Entity<POSITION_GP>(entity =>
        {
            entity.HasKey(e => e.ID_POSITION);

            entity.ToTable("POSITION_GPS");

            entity.HasIndex(e => new { e.ID_UTILISATEUR, e.HORODATAGE }, "IX_POSITION_UTILISATEUR_HORODATAGE").IsDescending(false, true);

            entity.Property(e => e.ID_POSITION).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.HORODATAGE).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.LATITUDE).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.LONGITUDE).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.PRECISION_METRES).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.ID_UTILISATEURNavigation).WithMany(p => p.POSITION_GPs)
                .HasForeignKey(d => d.ID_UTILISATEUR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_POSITION_UTILISATEUR");
        });

        modelBuilder.Entity<REF_CATEGORIE_DEPENSE>(entity =>
        {
            entity.HasKey(e => e.CODE).HasName("PK__REF_CATE__AA1D4378FA78616B");

            entity.ToTable("REF_CATEGORIE_DEPENSE");

            entity.Property(e => e.CODE).HasMaxLength(20);
            entity.Property(e => e.ACTIF)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_ROLE>(entity =>
        {
            entity.HasKey(e => e.CODE).HasName("PK__REF_ROLE__AA1D4378C93CC60E");

            entity.ToTable("REF_ROLE");

            entity.Property(e => e.CODE).HasMaxLength(20);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_STATUT_FACTURE>(entity =>
        {
            entity.HasKey(e => e.CODE).HasName("PK__REF_STAT__AA1D4378B9EA8A77");

            entity.ToTable("REF_STATUT_FACTURE");

            entity.Property(e => e.CODE).HasMaxLength(30);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_STATUT_INTERVENTION>(entity =>
        {
            entity.HasKey(e => e.CODE).HasName("PK__REF_STAT__AA1D4378B9A3AD91");

            entity.ToTable("REF_STATUT_INTERVENTION");

            entity.Property(e => e.CODE).HasMaxLength(20);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_STATUT_VEHICULE>(entity =>
        {
            entity.HasKey(e => e.CODE);

            entity.ToTable("REF_STATUT_VEHICULE");

            entity.Property(e => e.CODE).HasMaxLength(30);
            entity.Property(e => e.COULEUR_HEX)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_TYPE_CONTRAT>(entity =>
        {
            entity.HasKey(e => e.CODE).HasName("PK__REF_TYPE__AA1D437894C08930");

            entity.ToTable("REF_TYPE_CONTRAT");

            entity.Property(e => e.CODE).HasMaxLength(20);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_TYPE_DEPENSE_INTERVENTION>(entity =>
        {
            entity.HasKey(e => e.CODE).HasName("PK__REF_TYPE__AA1D4378F88DB337");

            entity.ToTable("REF_TYPE_DEPENSE_INTERVENTION");

            entity.Property(e => e.CODE).HasMaxLength(20);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_TYPE_DEPENSE_VEHICULE>(entity =>
        {
            entity.HasKey(e => e.CODE);

            entity.ToTable("REF_TYPE_DEPENSE_VEHICULE");

            entity.Property(e => e.CODE).HasMaxLength(30);
            entity.Property(e => e.EST_DEDUCTIBLE)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ICONE).HasMaxLength(10);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_TYPE_ENTRETIEN>(entity =>
        {
            entity.HasKey(e => e.CODE);

            entity.ToTable("REF_TYPE_ENTRETIEN");

            entity.Property(e => e.CODE).HasMaxLength(30);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_TYPE_INTERVENTION>(entity =>
        {
            entity.HasKey(e => e.CODE).HasName("PK__REF_TYPE__AA1D437878CB7841");

            entity.ToTable("REF_TYPE_INTERVENTION");

            entity.Property(e => e.CODE).HasMaxLength(20);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_TYPE_MOUVEMENT>(entity =>
        {
            entity.HasKey(e => e.CODE).HasName("PK__REF_TYPE__AA1D4378B29DE717");

            entity.ToTable("REF_TYPE_MOUVEMENT");

            entity.Property(e => e.CODE).HasMaxLength(10);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_TYPE_PHOTO>(entity =>
        {
            entity.HasKey(e => e.CODE).HasName("PK__REF_TYPE__AA1D43781E03B93B");

            entity.ToTable("REF_TYPE_PHOTO");

            entity.Property(e => e.CODE).HasMaxLength(20);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_TYPE_PLAN>(entity =>
        {
            entity.HasKey(e => e.CODE).HasName("PK__REF_TYPE__AA1D4378CFD0CA3A");

            entity.ToTable("REF_TYPE_PLAN");

            entity.Property(e => e.CODE).HasMaxLength(20);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<REF_TYPE_VEHICULE>(entity =>
        {
            entity.HasKey(e => e.CODE);

            entity.ToTable("REF_TYPE_VEHICULE");

            entity.Property(e => e.CODE).HasMaxLength(30);
            entity.Property(e => e.LIBELLE).HasMaxLength(100);
        });

        modelBuilder.Entity<UTILISATEUR>(entity =>
        {
            entity.HasKey(e => e.ID_UTILISATEUR);

            entity.ToTable("UTILISATEUR", tb => tb.HasTrigger("TRG_UTILISATEUR_UPDATE"));

            entity.HasIndex(e => new { e.ID_LOCATAIRE, e.TELEPHONE }, "UQ_UTILISATEUR_TEL_LOCATAIRE").IsUnique();

            entity.Property(e => e.ID_UTILISATEUR).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.DATE_CREATION).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.EMAIL).HasMaxLength(200);
            entity.Property(e => e.EST_ACTIF)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.MOT_DE_PASSE_HASH).HasMaxLength(500);
            entity.Property(e => e.NOM_COMPLET).HasMaxLength(200);
            entity.Property(e => e.ROLE)
                .HasMaxLength(20)
                .HasDefaultValueSql("('TECHNICIEN')");
            entity.Property(e => e.TELEPHONE).HasMaxLength(20);
            entity.Property(e => e.TOKEN_FCM).HasMaxLength(500);

            entity.HasOne(d => d.ID_LOCATAIRENavigation).WithMany(p => p.UTILISATEURs)
                .HasForeignKey(d => d.ID_LOCATAIRE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UTILISATEUR_LOCATAIRE");

            entity.HasOne(d => d.ROLENavigation).WithMany(p => p.UTILISATEURs)
                .HasForeignKey(d => d.ROLE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UTILISATEUR_ROLE");
        });

        modelBuilder.Entity<VEHICULE>(entity =>
        {
            entity.HasKey(e => e.ID_VEHICULE);

            entity.ToTable("VEHICULE");

            entity.HasIndex(e => new { e.ID_LOCATAIRE, e.ASSURANCE_EXPIRATION, e.VISITE_TECHNIQUE_EXPIRATION }, "IX_VEHICULE_DOCS_EXPIRATION").HasFilter("([EST_SUPPRIME]=(0))");

            entity.HasIndex(e => new { e.ID_LOCATAIRE, e.STATUT }, "IX_VEHICULE_LOCATAIRE_STATUT").HasFilter("([EST_SUPPRIME]=(0))");

            entity.HasIndex(e => new { e.ID_LOCATAIRE, e.IMMATRICULATION }, "UQ_VEHICULE_IMMAT").IsUnique();

            entity.Property(e => e.ID_VEHICULE).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ASSURANCE_COMPAGNIE).HasMaxLength(200);
            entity.Property(e => e.ASSURANCE_EXPIRATION).HasColumnType("date");
            entity.Property(e => e.ASSURANCE_NUMERO).HasMaxLength(100);
            entity.Property(e => e.COULEUR).HasMaxLength(50);
            entity.Property(e => e.DATE_ACQUISITION).HasColumnType("date");
            entity.Property(e => e.DATE_CREATION).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IMMATRICULATION).HasMaxLength(20);
            entity.Property(e => e.MARQUE).HasMaxLength(100);
            entity.Property(e => e.MODELE).HasMaxLength(100);
            entity.Property(e => e.PRIX_ACQUISITION_XAF).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.STATUT)
                .HasMaxLength(30)
                .HasDefaultValueSql("('DISPONIBLE')");
            entity.Property(e => e.TYPE_VEHICULE).HasMaxLength(30);
            entity.Property(e => e.URL_PHOTO).HasMaxLength(500);
            entity.Property(e => e.VIGNETTE_EXPIRATION).HasColumnType("date");
            entity.Property(e => e.VISITE_TECHNIQUE_EXPIRATION).HasColumnType("date");

            entity.HasOne(d => d.ID_CREATEURNavigation).WithMany(p => p.VEHICULEs)
                .HasForeignKey(d => d.ID_CREATEUR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICULE_CREATEUR");

            entity.HasOne(d => d.ID_LOCATAIRENavigation).WithMany(p => p.VEHICULEs)
                .HasForeignKey(d => d.ID_LOCATAIRE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICULE_LOCATAIRE");

            entity.HasOne(d => d.STATUTNavigation).WithMany(p => p.VEHICULEs)
                .HasForeignKey(d => d.STATUT)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICULE_STATUT");

            entity.HasOne(d => d.TYPE_VEHICULENavigation).WithMany(p => p.VEHICULEs)
                .HasForeignKey(d => d.TYPE_VEHICULE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICULE_TYPE");
        });

        modelBuilder.Entity<VUE_ARTICLES_ALERTE_STOCK>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VUE_ARTICLES_ALERTE_STOCK");

            entity.Property(e => e.NOM_ARTICLE).HasMaxLength(200);
            entity.Property(e => e.NOM_LOCATAIRE).HasMaxLength(200);
            entity.Property(e => e.PRIX_UNITAIRE_XAF).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.REFERENCE).HasMaxLength(100);
            entity.Property(e => e.STOCK_ACTUEL).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.STOCK_MINIMUM).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.UNITE).HasMaxLength(30);
        });

        modelBuilder.Entity<VUE_FACTURES_EN_RETARD>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VUE_FACTURES_EN_RETARD");

            entity.Property(e => e.DATE_ECHEANCE).HasColumnType("date");
            entity.Property(e => e.NOM_CLIENT).HasMaxLength(200);
            entity.Property(e => e.NOM_LOCATAIRE).HasMaxLength(200);
            entity.Property(e => e.REFERENCE).HasMaxLength(30);
            entity.Property(e => e.TOTAL_XAF).HasColumnType("decimal(15, 2)");
        });

        modelBuilder.Entity<VUE_INTERVENTION_TECHNICIEN>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VUE_INTERVENTION_TECHNICIEN");

            entity.Property(e => e.NOM_CLIENT).HasMaxLength(200);
            entity.Property(e => e.NOM_CREATEUR).HasMaxLength(200);
            entity.Property(e => e.NOM_TECHNICIEN_PRINCIPAL).HasMaxLength(200);
            entity.Property(e => e.REFERENCE).HasMaxLength(30);
            entity.Property(e => e.STATUT).HasMaxLength(20);
            entity.Property(e => e.TEL_TECHNICIEN_PRINCIPAL).HasMaxLength(20);
            entity.Property(e => e.TITRE).HasMaxLength(300);
            entity.Property(e => e.TYPE).HasMaxLength(20);
        });

        modelBuilder.Entity<V_ALERTES_VEHICULE>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_ALERTES_VEHICULE");

            entity.Property(e => e.IMMATRICULATION).HasMaxLength(20);
            entity.Property(e => e.MESSAGE).HasMaxLength(4000);
            entity.Property(e => e.NIVEAU)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.TYPE_ALERTE)
                .HasMaxLength(19)
                .IsUnicode(false);
        });

        modelBuilder.Entity<V_DEPENSES_PAR_VEHICULE>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_DEPENSES_PAR_VEHICULE");

            entity.Property(e => e.CONTEXTE)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.DATE_DEPENSE).HasColumnType("date");
            entity.Property(e => e.DESCRIPTION).HasMaxLength(500);
            entity.Property(e => e.IMMATRICULATION).HasMaxLength(20);
            entity.Property(e => e.INTERVENTION_REFERENCE).HasMaxLength(30);
            entity.Property(e => e.INTERVENTION_TITRE).HasMaxLength(300);
            entity.Property(e => e.MONTANT_XAF).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SAISIE_PAR).HasMaxLength(200);
            entity.Property(e => e.TYPE_DEPENSE).HasMaxLength(30);
            entity.Property(e => e.TYPE_DEPENSE_LIBELLE).HasMaxLength(100);
            entity.Property(e => e.URL_JUSTIFICATIF).HasMaxLength(500);
            entity.Property(e => e.VEHICULE).HasMaxLength(201);
        });

        modelBuilder.Entity<V_VEHICULE_DASHBOARD>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_VEHICULE_DASHBOARD");

            entity.Property(e => e.ASSURANCE_EXPIRATION).HasColumnType("date");
            entity.Property(e => e.COULEUR_HEX)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DEPENSES_EN_MISSION_XAF).HasColumnType("decimal(38, 2)");
            entity.Property(e => e.DEPENSES_HORS_MISSION_XAF).HasColumnType("decimal(38, 2)");
            entity.Property(e => e.DESIGNATION).HasMaxLength(201);
            entity.Property(e => e.IMMATRICULATION).HasMaxLength(20);
            entity.Property(e => e.STATUT).HasMaxLength(30);
            entity.Property(e => e.STATUT_LIBELLE).HasMaxLength(100);
            entity.Property(e => e.TOTAL_DEPENSES_XAF).HasColumnType("decimal(38, 2)");
            entity.Property(e => e.TYPE_VEHICULE).HasMaxLength(30);
            entity.Property(e => e.VIGNETTE_EXPIRATION).HasColumnType("date");
            entity.Property(e => e.VISITE_TECHNIQUE_EXPIRATION).HasColumnType("date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
