using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperNumberProject.Models;

public partial class SuperNumberdbContext : DbContext
{
    public SuperNumberdbContext()
    {
    }

    public SuperNumberdbContext(DbContextOptions<SuperNumberdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SuperNumber> SuperNumbers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSnumero> UserSnumeros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BORJA\\SQLEXPRESS;Database=SuperNumberdb;User Id=kAdmin;Password=kadmin1;TrustServerCertificate=True;Connection Timeout=300;Persist Security Info=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SuperNumber>(entity =>
        {
            entity.ToTable("SuperNumber");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.SuperNumber1).HasColumnName("superNumber");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Name, "CN_Name").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PassHash)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("passHash");
            entity.Property(e => e.RegiDate)
                .HasColumnType("datetime")
                .HasColumnName("regiDate");
        });

        modelBuilder.Entity<UserSnumero>(entity =>
        {
            entity.ToTable("UserSNumero");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdSuperNumber).HasColumnName("idSuperNumber");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.RegDate)
                .HasColumnType("datetime")
                .HasColumnName("regDate");

            entity.HasOne(d => d.IdSuperNumberNavigation).WithMany(p => p.UserSnumeros)
                .HasForeignKey(d => d.IdSuperNumber)
                .HasConstraintName("FK_UserSNumero_SuperNumber");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserSnumeros)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_UserSNumero_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
