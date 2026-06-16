using System;
using System.Collections.Generic;
using BoscoAFH.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BoscoAFH.Entities.Data;

public partial class BoscoAFHDbContext : DbContext
{
    public BoscoAFHDbContext(DbContextOptions<BoscoAFHDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ModuleRight> ModuleRights { get; set; }

    public virtual DbSet<ModulesAndFeature> ModulesAndFeatures { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ModuleRight>(entity =>
        {
            entity.HasKey(e => e.RightsId).HasName("module_rights_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Feature).WithMany(p => p.ModuleRights).HasConstraintName("fk_module_rights_feature");

            entity.HasOne(d => d.Role).WithMany(p => p.ModuleRights)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_module_rights_roles");

            entity.HasOne(d => d.User).WithMany(p => p.ModuleRights)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_module_rights_users");
        });

        modelBuilder.Entity<ModulesAndFeature>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("modules_and_features_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsShowInRights).HasDefaultValue(true);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_modules_parent");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
