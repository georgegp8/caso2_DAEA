using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Caso2_EmpresaServicioConsultoria.Models;

public partial class ConsultoriaDBContext : DbContext
{
    public ConsultoriaDBContext()
    {
    }

    public ConsultoriaDBContext(DbContextOptions<ConsultoriaDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Comunicacione> Comunicaciones { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Gasto> Gastos { get; set; }

    public virtual DbSet<Hito> Hitos { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<VDashboardProyecto> VDashboardProyectos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=ConsultoriaDB;Username=postgres;Password=1234;Port=5432");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clientes_pkey");

            entity.ToTable("clientes");

            entity.HasIndex(e => e.Email, "clientes_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Empresa)
                .HasMaxLength(200)
                .HasColumnName("empresa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Comunicacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comunicaciones_pkey");

            entity.ToTable("comunicaciones");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Asunto)
                .HasMaxLength(300)
                .HasColumnName("asunto");
            entity.Property(e => e.Contenido).HasColumnName("contenido");
            entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");
            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(30)
                .HasColumnName("tipo");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Comunicaciones)
                .HasForeignKey(d => d.EmpleadoId)
                .HasConstraintName("comunicaciones_empleado_id_fkey");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Comunicaciones)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("comunicaciones_proyecto_id_fkey");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("empleados_pkey");

            entity.ToTable("empleados");

            entity.HasIndex(e => e.Email, "empleados_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Cargo)
                .HasMaxLength(100)
                .HasColumnName("cargo");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(30)
                .HasColumnName("rol");
        });

        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gastos_pkey");

            entity.ToTable("gastos");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(60)
                .HasDefaultValueSql("'otros'::character varying")
                .HasColumnName("categoria");
            entity.Property(e => e.Concepto)
                .HasMaxLength(250)
                .HasColumnName("concepto");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("fecha");
            entity.Property(e => e.Monto)
                .HasPrecision(12, 2)
                .HasColumnName("monto");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("gastos_proyecto_id_fkey");
        });

        modelBuilder.Entity<Hito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hitos_pkey");

            entity.ToTable("hitos");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Completado)
                .HasDefaultValue(false)
                .HasColumnName("completado");
            entity.Property(e => e.FechaPlanificada).HasColumnName("fecha_planificada");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Hitos)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("hitos_proyecto_id_fkey");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("proyectos_pkey");

            entity.ToTable("proyectos");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasDefaultValueSql("'planificacion'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaFinEstimada).HasColumnName("fecha_fin_estimada");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.PresupuestoTotal)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("presupuesto_total");
            entity.Property(e => e.ResponsableId).HasColumnName("responsable_id");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("proyectos_cliente_id_fkey");

            entity.HasOne(d => d.Responsable).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.ResponsableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("proyectos_responsable_id_fkey");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tareas_pkey");

            entity.ToTable("tareas");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AsignadoA).HasColumnName("asignado_a");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasDefaultValueSql("'pendiente'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(15)
                .HasDefaultValueSql("'media'::character varying")
                .HasColumnName("prioridad");
            entity.Property(e => e.ProgresoPct)
                .HasDefaultValue((short)0)
                .HasColumnName("progreso_pct");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .HasColumnName("titulo");

            entity.HasOne(d => d.AsignadoANavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.AsignadoA)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tareas_asignado_a_fkey");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("tareas_proyecto_id_fkey");
        });

        modelBuilder.Entity<VDashboardProyecto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_dashboard_proyectos");

            entity.Property(e => e.AvanceTareasPct).HasColumnName("avance_tareas_pct");
            entity.Property(e => e.Cliente)
                .HasMaxLength(150)
                .HasColumnName("cliente");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasColumnName("estado");
            entity.Property(e => e.GastoEjecutado).HasColumnName("gasto_ejecutado");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LiderProyecto)
                .HasMaxLength(150)
                .HasColumnName("lider_proyecto");
            entity.Property(e => e.Presupuesto)
                .HasPrecision(14, 2)
                .HasColumnName("presupuesto");
            entity.Property(e => e.Proyecto)
                .HasMaxLength(200)
                .HasColumnName("proyecto");
            entity.Property(e => e.Saldo).HasColumnName("saldo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
