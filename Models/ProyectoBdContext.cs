using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Models;

public partial class ProyectoBdContext : DbContext
{
    public ProyectoBdContext()
    {
    }

    public ProyectoBdContext(DbContextOptions<ProyectoBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Cobro> Cobros { get; set; }

    public virtual DbSet<Consultorio> Consultorios { get; set; }

    public virtual DbSet<Devolucione> Devoluciones { get; set; }

    public virtual DbSet<Doctore> Doctores { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Especialidadad> Especialidadads { get; set; }

    public virtual DbSet<HistoriaMedica> HistoriaMedicas { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Recetum> Receta { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VerCita> VerCitas { get; set; }

    public virtual DbSet<VerHistoriaMedica> VerHistoriaMedicas { get; set; }

    public virtual DbSet<VerHorariosDoctore> VerHorariosDoctores { get; set; }

    public virtual DbSet<VerNombresDoctore> VerNombresDoctores { get; set; }

    public virtual DbSet<VerNombresEmpleado> VerNombresEmpleados { get; set; }

    public virtual DbSet<VerNombresPaciente> VerNombresPacientes { get; set; }

    public virtual DbSet<VerReceta> VerRecetas { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.FolioCita);

            entity.Property(e => e.FolioCita).HasColumnName("folioCita");
            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estatus");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Citas_Doctores");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Citas_Pacientes_1");
        });

        modelBuilder.Entity<Cobro>(entity =>
        {
            entity.HasKey(e => e.IdCobro).HasName("PK__Cobro__D0B0CD00CF11B64B");

            entity.ToTable("Cobro");

            entity.Property(e => e.IdTicket).HasColumnName("idTicket");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Cobros)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__Cobro__IdEmplead__31B762FC");

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.Cobros)
                .HasForeignKey(d => d.IdTicket)
                .HasConstraintName("FK__Cobro__idTicket__30C33EC3");
        });

        modelBuilder.Entity<Consultorio>(entity =>
        {
            entity.HasKey(e => new { e.NumeroConsultorio, e.IdDoctor, e.HoraOcupacion, e.HoraLiberacion });

            entity.Property(e => e.NumeroConsultorio)
                .ValueGeneratedOnAdd()
                .HasColumnName("numeroConsultorio");
            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.HoraOcupacion)
                .HasColumnType("datetime")
                .HasColumnName("horaOcupacion");
            entity.Property(e => e.HoraLiberacion)
                .HasColumnType("datetime")
                .HasColumnName("horaLiberacion");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Consultorios)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Consultorios_Doctores");
        });

        modelBuilder.Entity<Devolucione>(entity =>
        {
            entity.HasKey(e => e.FolioDevolucion).HasName("PK__devoluci__485AC239810F15CC");

            entity.ToTable("devoluciones");

            entity.Property(e => e.FolioDevolucion).HasColumnName("folioDevolucion");
            entity.Property(e => e.FechaDevolucion)
                .HasColumnType("datetime")
                .HasColumnName("fechaDevolucion");
            entity.Property(e => e.FolioPago).HasColumnName("folioPago");
            entity.Property(e => e.MontoDevuelto)
                .HasColumnType("money")
                .HasColumnName("montoDevuelto");

            entity.HasOne(d => d.FolioPagoNavigation).WithMany(p => p.Devoluciones)
                .HasForeignKey(d => d.FolioPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_devoluciones_Pago");
        });

        modelBuilder.Entity<Doctore>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("PK__Doctores__418956C31AF176E4");

            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Doctores)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctores__idEmpl__07C12930");

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Doctores)
                .HasForeignKey(d => d.IdEspecialidad)
                .HasConstraintName("FK_Doctores_Especialidadad");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9E2F3D172D");

            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaContratacion");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.CurpNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.Curp)
                .HasConstraintName("FK_Empleados_Persona");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdHorario)
                .HasConstraintName("FK_EMPLEADOS_IdHorario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Empleados_Usuarios");
        });

        modelBuilder.Entity<Especialidadad>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad);

            entity.ToTable("Especialidadad");

            entity.HasIndex(e => e.CobroPorConsulta, "coborPorConsulta").IsUnique();

            entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");
            entity.Property(e => e.CobroPorConsulta)
                .HasColumnType("money")
                .HasColumnName("cobroPorConsulta");
            entity.Property(e => e.NombreEspecialidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreEspecialidad");
        });

        modelBuilder.Entity<HistoriaMedica>(entity =>
        {
            entity.HasKey(e => e.IdHistoriaMedica);

            entity.ToTable("HistoriaMedica");

            entity.Property(e => e.IdHistoriaMedica).HasColumnName("idHistoriaMedica");
            entity.Property(e => e.Alergias)
                .IsUnicode(false)
                .HasColumnName("alergias");
            entity.Property(e => e.Estatura)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("estatura");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.PadecimientosPrevios)
                .IsUnicode(false)
                .HasColumnName("padecimientosPrevios");
            entity.Property(e => e.Peso)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("peso");
            entity.Property(e => e.TipoSangre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipoSangre");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK__Horarios__1539229B509970BC");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.IdMedicamento).HasName("PK__Medicame__42B24C587BD9D95E");

            entity.ToTable("Medicamento");

            entity.Property(e => e.IdMedicamento).HasColumnName("idMedicamento");
            entity.Property(e => e.CantidadEnStock).HasColumnName("cantidadEnStock");
            entity.Property(e => e.NombreMedicamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreMedicamento");
            entity.Property(e => e.PrecioMedicamento)
                .HasColumnType("money")
                .HasColumnName("precioMedicamento");
            entity.Property(e => e.Tratamiento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tratamiento");
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion).HasName("PK_Notificaiones");

            entity.Property(e => e.IdNotificacion).HasColumnName("idNotificacion");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Leida).HasColumnName("leida");
            entity.Property(e => e.Mensaje)
                .IsUnicode(false)
                .HasColumnName("mensaje");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notificaiones_Usuarios");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__Paciente__F48A08F27F279B8D");

            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.CurpNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.Curp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pacientes_Persona");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Pacientes_Usuarios");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.FolioPago).HasName("PK__Pago__8BD981CBD6766CD7");

            entity.ToTable("Pago");

            entity.Property(e => e.FolioPago).HasColumnName("folioPago");
            entity.Property(e => e.EstatusPago)
                .HasMaxLength(50)
                .HasColumnName("estatusPago");
            entity.Property(e => e.FechaPago)
                .HasColumnType("datetime")
                .HasColumnName("fechaPago");
            entity.Property(e => e.FolioCita).HasColumnName("folioCita");
            entity.Property(e => e.MontoPagado)
                .HasColumnType("money")
                .HasColumnName("montoPagado");

            entity.HasOne(d => d.FolioCitaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.FolioCita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__folioCita__42E1EEFE");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Curp);

            entity.ToTable("Persona");

            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidoMaterno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidoPaterno");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primerNombre");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("segundoNombre");
        });

        modelBuilder.Entity<Recetum>(entity =>
        {
            entity.HasKey(e => new { e.FolioReceta, e.Medicamentos }).HasName("PK__tmp_ms_x__B6C45C7DB54F18B4");

            entity.Property(e => e.FolioReceta).HasColumnName("folioReceta");
            entity.Property(e => e.Medicamentos)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("medicamentos");
            entity.Property(e => e.FechaReceta)
                .HasColumnType("datetime")
                .HasColumnName("fechaReceta");
            entity.Property(e => e.FolioCita).HasColumnName("folioCita");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("observaciones");

            entity.HasOne(d => d.FolioCitaNavigation).WithMany(p => p.Receta)
                .HasForeignKey(d => d.FolioCita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receta_Citas");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__CEB9811910C2E194");

            entity.ToTable("Servicio");

            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.EstatusServicio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estatusServicio");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreServicio");
            entity.Property(e => e.PrecioServicio)
                .HasColumnType("money")
                .HasColumnName("precioServicio");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.IdTicket).HasName("PK__Ticket__22B1456F63C1DA93");

            entity.ToTable("Ticket");

            entity.Property(e => e.IdTicket).HasColumnName("idTicket");
            entity.Property(e => e.FechaTicket)
                .HasColumnType("datetime")
                .HasColumnName("fechaTicket");
            entity.Property(e => e.IdMedicamento).HasColumnName("idMedicamento");
            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.NumeroMedicamentos).HasColumnName("numeroMedicamentos");
            entity.Property(e => e.NumeroServicios).HasColumnName("numeroServicios");

            entity.HasOne(d => d.IdMedicamentoNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdMedicamento)
                .HasConstraintName("FK__Ticket__idMedica__2DE6D218");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("FK__Ticket__idServic__2BFE89A6");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__5B65BF972F8CB7D3");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__A0436BD7F7BD9BDE").IsUnique();

            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("mail con el que se autoriza")
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Permiso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("rol que desempeña al usar la bd")
                .HasColumnName("permiso");
        });

        modelBuilder.Entity<VerCita>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("verCitas");

            entity.Property(e => e.Doctor)
                .HasMaxLength(203)
                .IsUnicode(false);
            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estatus");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FolioCita).HasColumnName("folioCita");
            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.Paciente)
                .HasMaxLength(203)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VerHistoriaMedica>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("verHistoriaMedica");

            entity.Property(e => e.Alergias)
                .IsUnicode(false)
                .HasColumnName("alergias");
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Estatura)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("estatura");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(203)
                .IsUnicode(false)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.PadecimientosPrevios)
                .IsUnicode(false)
                .HasColumnName("padecimientosPrevios");
            entity.Property(e => e.Peso)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("peso");
            entity.Property(e => e.TipoSangre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipoSangre");
        });

        modelBuilder.Entity<VerHorariosDoctore>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("verHorariosDoctores");

            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
        });

        modelBuilder.Entity<VerNombresDoctore>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("verNombresDoctores");

            entity.Property(e => e.CobroPorConsulta)
                .HasColumnType("money")
                .HasColumnName("cobroPorConsulta");
            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(203)
                .IsUnicode(false);
            entity.Property(e => e.NombreEspecialidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreEspecialidad");
        });

        modelBuilder.Entity<VerNombresEmpleado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("verNombresEmpleados");

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(203)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VerNombresPaciente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("verNombresPacientes");

            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(203)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VerReceta>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("verRecetas");

            entity.Property(e => e.Doctor)
                .HasMaxLength(203)
                .IsUnicode(false);
            entity.Property(e => e.FolioReceta).HasColumnName("folioReceta");
            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.Medicamentos)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("medicamentos");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("observaciones");
            entity.Property(e => e.Paciente)
                .HasMaxLength(203)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
