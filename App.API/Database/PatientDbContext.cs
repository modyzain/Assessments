using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App.API.Database
{
    public partial class PatientDbContext : DbContext
    {
        public PatientDbContext()
        {
        }

        public PatientDbContext(DbContextOptions<PatientDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GpDetails> GpDetails { get; set; }
        public virtual DbSet<NextOfKin> NextOfKin { get; set; }
        public virtual DbSet<NokRelationship> NokRelationship { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=.;Database=PatientDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GpDetails>(entity =>
            {
                entity.HasKey(e => e.PkGpDetailsId);

                entity.Property(e => e.GpCode).HasMaxLength(50);

                entity.Property(e => e.GpInitials).HasMaxLength(100);

                entity.Property(e => e.GpPhone).HasMaxLength(15);

                entity.Property(e => e.GpSurname).HasMaxLength(150);
            });

            modelBuilder.Entity<NextOfKin>(entity =>
            {
                entity.HasKey(e => e.PkNokId);

                entity.Property(e => e.NokAddressLine1).HasMaxLength(150);

                entity.Property(e => e.NokAddressLine2).HasMaxLength(150);

                entity.Property(e => e.NokAddressLine3).HasMaxLength(150);

                entity.Property(e => e.NokAddressLine4).HasMaxLength(150);

                entity.Property(e => e.NokName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.NokPostcode).HasMaxLength(25);

                entity.HasOne(d => d.FkNokRelationship)
                    .WithMany(p => p.NextOfKin)
                    .HasForeignKey(d => d.FkNokRelationshipId)
                    .HasConstraintName("FK_NextOfKin_NokRelationship");

                entity.HasOne(d => d.FkPatient)
                    .WithMany(p => p.NextOfKin)
                    .HasForeignKey(d => d.FkPatientId)
                    .HasConstraintName("FK_NextOfKin_Patients");
            });

            modelBuilder.Entity<NokRelationship>(entity =>
            {
                entity.HasKey(e => e.PkNokRelationshipId);

                entity.Property(e => e.PkNokRelationshipId).ValueGeneratedOnAdd();

                entity.Property(e => e.NokRelationshipCode)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Patients>(entity =>
            {
                entity.HasKey(e => e.PkPatientId);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.ForeName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.HomeTelephoneNumber).HasMaxLength(15);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
