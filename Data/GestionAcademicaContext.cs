using Microsoft.EntityFrameworkCore;
using API_REST_CURSOSACADEMICOS.Models;

namespace API_REST_CURSOSACADEMICOS.Data
{
    public class GestionAcademicaContext : DbContext
    {
        public GestionAcademicaContext(DbContextOptions<GestionAcademicaContext> options) : base(options)
        {
        }

        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Curso> Cursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para Docente
            modelBuilder.Entity<Docente>(entity =>
            {
                entity.ToTable("Docente");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Apellidos).HasColumnName("apellidos").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Nombres).HasColumnName("nombres").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Profesion).HasColumnName("profesion").HasMaxLength(100);
                entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento").HasColumnType("date");
                entity.Property(e => e.Correo).HasColumnName("correo").HasMaxLength(100);

                // Índice único para el correo
                entity.HasIndex(e => e.Correo).IsUnique();
            });

            // Configuración para Curso
            modelBuilder.Entity<Curso>(entity =>
            {
                entity.ToTable("Curso");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.NombreCurso).HasColumnName("curso").IsRequired().HasMaxLength(200);
                entity.Property(e => e.Creditos).HasColumnName("creditos").IsRequired();
                entity.Property(e => e.HorasSemanal).HasColumnName("horasSemanal").IsRequired();
                entity.Property(e => e.Ciclo).HasColumnName("ciclo").HasMaxLength(50);
                entity.Property(e => e.IdDocente).HasColumnName("idDocente");

                // Configuración de relación con Docente
                entity.HasOne(c => c.Docente)
                      .WithMany(d => d.Cursos)
                      .HasForeignKey(c => c.IdDocente)
                      .HasConstraintName("FK_Curso_Docente")
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
