using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ISAD251REF.Models
{
    public partial class ISAD251_RHarrisContext : DbContext
    {
        public ISAD251_RHarrisContext()
        {
        }

        public ISAD251_RHarrisContext(DbContextOptions<ISAD251_RHarrisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppointmentTypes> AppointmentTypes { get; set; }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<FamilyMembers> FamilyMembers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=socem1.uopnet.plymouth.ac.uk; Database=ISAD251_RHarris; User Id=RHarris; Password=ISAD251_22217061");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentTypes>(entity =>
            {
                entity.Property(e => e.AppointmentTypesId).HasColumnName("AppointmentTypesID");

                entity.Property(e => e.AppointmentTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.HasKey(e => e.AppointmentId)
                    .HasName("PK__Appointm__8ECDFCA2C89D776E");

                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.Property(e => e.AppointmentNotes).HasMaxLength(501);

                entity.Property(e => e.AppointmentTypeId).HasColumnName("AppointmentTypeID");

                entity.Property(e => e.FamilyMemberId).HasColumnName("FamilyMemberID");

                entity.HasOne(d => d.AppointmentType)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AppointmentTypeId)
                    .HasConstraintName("FK__Appointme__Appoi__07C12930");

                entity.HasOne(d => d.FamilyMember)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.FamilyMemberId)
                    .HasConstraintName("FK__Appointme__Famil__06CD04F7");
            });

            modelBuilder.Entity<FamilyMembers>(entity =>
            {
                entity.HasKey(e => e.FamilyMemberId)
                    .HasName("PK__FamilyMe__B7AD6DF39A1EE904");

                entity.Property(e => e.FamilyMemberId).HasColumnName("FamilyMemberID");

                entity.Property(e => e.FamilyMemberName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
