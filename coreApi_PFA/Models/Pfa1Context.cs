using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace coreApi_PFA.Models
{
    public partial class Pfa1Context : DbContext
    {
        public Pfa1Context()
        {
        }

        public Pfa1Context(DbContextOptions<Pfa1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Absence> Absence { get; set; }
        public virtual DbSet<Administrateur> Administrateur { get; set; }
        public virtual DbSet<Affectation> Affectation { get; set; }
        public virtual DbSet<AnneeScolaire> AnneeScolaire { get; set; }
        public virtual DbSet<Enseignant> Enseignant { get; set; }
        public virtual DbSet<Etudiant> Etudiant { get; set; }
        public virtual DbSet<Filiere> Filiere { get; set; }
        public virtual DbSet<Matiere> Matiere { get; set; }
        public virtual DbSet<Seance> Seance { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("server=MIC;database=Pfa1;integrated security=true");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Absence>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasColumnName("_Date")
                    .HasColumnType("date");

                entity.Property(e => e.IdEtud).HasColumnName("Id_Etud");

                entity.Property(e => e.IdSeance).HasColumnName("Id_Seance");

                entity.HasOne(d => d.IdEtudNavigation)
                    .WithMany(p => p.Absence)
                    .HasForeignKey(d => d.IdEtud)
                    .HasConstraintName("FK__Absence__Id_Etud__2A4B4B5E");

                entity.HasOne(d => d.IdSeanceNavigation)
                    .WithMany(p => p.Absence)
                    .HasForeignKey(d => d.IdSeance)
                    .HasConstraintName("FK__Absence__Id_Sean__2B3F6F97");
            });

            modelBuilder.Entity<Administrateur>(entity =>
            {
                entity.Property(e => e.Adresse)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cne)
                    .HasColumnName("CNE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateNais)
                    .HasColumnName("Date_nais")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Genre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MotdePasse)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Affectation>(entity =>
            {
                entity.HasKey(e => new { e.IdFiliere, e.IdMatiere })
                    .HasName("PK__Affectat__302FDF00D7B9B150");

                entity.Property(e => e.IdFiliere).HasColumnName("Id_Filiere");

                entity.Property(e => e.IdMatiere).HasColumnName("Id_Matiere");

                entity.Property(e => e.IdEns).HasColumnName("Id_Ens");

                entity.HasOne(d => d.IdEnsNavigation)
                    .WithMany(p => p.Affectation)
                    .HasForeignKey(d => d.IdEns)
                    .HasConstraintName("FK__Affectati__Id_En__22AA2996");

                entity.HasOne(d => d.IdFiliereNavigation)
                    .WithMany(p => p.Affectation)
                    .HasForeignKey(d => d.IdFiliere)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Affectati__Id_Fi__20C1E124");

                entity.HasOne(d => d.IdMatiereNavigation)
                    .WithMany(p => p.Affectation)
                    .HasForeignKey(d => d.IdMatiere)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Affectati__Id_Ma__21B6055D");
            });

            modelBuilder.Entity<AnneeScolaire>(entity =>
            {
                entity.ToTable("Annee_Scolaire");

                entity.Property(e => e.Annee)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Enseignant>(entity =>
            {
                entity.Property(e => e.Adresse)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cnss)
                    .HasColumnName("CNSS")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateEmb)
                    .HasColumnName("Date_Emb")
                    .HasColumnType("date");

                entity.Property(e => e.DateNais)
                    .HasColumnName("Date_nais")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Genre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MotdePasse)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Etudiant>(entity =>
            {
                entity.Property(e => e.Adresse)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cne)
                    .HasColumnName("CNE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateNais)
                    .HasColumnName("Date_nais")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Genre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdFiliere).HasColumnName("Id_Filiere");

                entity.Property(e => e.MotdePasse)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Approve)
                   .HasMaxLength(100)
                   .IsUnicode(false);

                entity.HasOne(d => d.IdFiliereNavigation)
                    .WithMany(p => p.Etudiant)
                    .HasForeignKey(d => d.IdFiliere)
                    .HasConstraintName("FK__Etudiant__Id_Fil__15502E78");
            });

            modelBuilder.Entity<Filiere>(entity =>
            {
                entity.Property(e => e.Libelle)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Matiere>(entity =>
            {
                entity.Property(e => e.Libelle)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Seance>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasColumnName("_Date")
                    .HasColumnType("date");

                entity.Property(e => e.IdFiliere).HasColumnName("Id_Filiere");

                entity.Property(e => e.IdMatiere).HasColumnName("Id_Matiere");

                entity.Property(e => e.Sujet)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Duree).HasColumnName("Duree");
                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Seance)
                    .HasForeignKey(d => new { d.IdFiliere, d.IdMatiere })
                    .HasConstraintName("FK__Seance__25869641");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.IdUti)
                    .HasName("PK__Utilisat__52A339DCF2CA6B37");

                entity.Property(e => e.IdUti).HasColumnName("Id_Uti");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MotdePasse)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("_Status")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
