using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infra.Domain
{
    public partial class testStoreContext : DbContext
    {
        public testStoreContext()
        {
        }

        public testStoreContext(DbContextOptions<testStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Modules> Modules { get; set; }
        public virtual DbSet<Nationality> Nationality { get; set; }
        public virtual DbSet<Permisstions> Permisstions { get; set; }
        public virtual DbSet<RolePermisstion> RolePermisstion { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=testStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cities>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.UserId).HasMaxLength(128);
            });

            modelBuilder.Entity<Modules>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Permisstions>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<RolePermisstion>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.PermisstionId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Permisstion)
                    .WithMany(p => p.RolePermisstion)
                    .HasForeignKey(d => d.PermisstionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermisstion_Permisstions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermisstion)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermisstion_Roles1");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.CreateUserId).HasMaxLength(128);

                entity.Property(e => e.ModifyAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_Roles_Users2");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.CityId).HasMaxLength(128);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.IdNumber).IsRequired();

                entity.Property(e => e.Mather).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.NatId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Phone).IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Suppliers_Cities");

                entity.HasOne(d => d.Nat)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.NatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Suppliers_Nationality1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Suppliers_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.CreateUserId).HasMaxLength(128);

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.GroupId).HasMaxLength(128);

                entity.Property(e => e.ModifyAt).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Users_Modules");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
