/*
 * Fecha de desarrollo: 10/12/2024
 * By: Xelan - AM
 * 
 * Uso de Entity Framework para conexión hacia una base de datos Postgres
 * 
 * "Una frase sencilla, para este momento tan complejo." Me.
 */

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CapaAccesoBD.Models;

public partial class BaseToDoContext : DbContext
{
    //Constructor por defecto del Contexto, se debe pasar un DbContextOptions 
    //ecnargado de dar la configuración y parámetos necesarios para la conexión 


    public BaseToDoContext(DbContextOptions<BaseToDoContext> options)
        : base(options)
    {
    }

    //Atributos

    public virtual DbSet<Asignation> Asignations { get; set; }

    public virtual DbSet<Categorylabel> Categorylabels { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    // sobrescritura de la estructura que infiere nuestro ORM
    // es decir conexiones, tuplas, tablas, llaves, etc.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignation>(entity =>
        {
            entity.HasKey(e => e.Idasignation).HasName("asignation_pkey");

            entity.ToTable("asignation");

            entity.Property(e => e.Idasignation).HasColumnName("idasignation");
            entity.Property(e => e.Idlabel).HasColumnName("idlabel");
            entity.Property(e => e.Idtask).HasColumnName("idtask");

            entity.HasOne(d => d.IdlabelNavigation).WithMany(p => p.Asignations)
                .HasForeignKey(d => d.Idlabel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignation_idlabel_fkey");

            entity.HasOne(d => d.IdtaskNavigation).WithMany(p => p.Asignations)
                .HasForeignKey(d => d.Idtask)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignation_idtask_fkey");
        });

        modelBuilder.Entity<Categorylabel>(entity =>
        {
            entity.HasKey(e => e.Idlabel).HasName("categorylabel_pkey");

            entity.ToTable("categorylabel");

            entity.Property(e => e.Idlabel).HasColumnName("idlabel");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Categorylabels)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("categorylabel_iduser_fkey");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Idtask).HasName("task_pkey");

            entity.ToTable("task");

            entity.Property(e => e.Idtask).HasColumnName("idtask");
            entity.Property(e => e.Creationdate).HasColumnName("creationdate");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Priority)
                .HasMaxLength(10)
                .HasColumnName("priority");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("task_iduser_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Email, "usuario_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "usuario_username_key").IsUnique();

            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
