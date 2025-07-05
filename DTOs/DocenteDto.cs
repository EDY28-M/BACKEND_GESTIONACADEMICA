using System.ComponentModel.DataAnnotations;

namespace API_REST_CURSOSACADEMICOS.DTOs
{
    public class DocenteDto
    {
        public int Id { get; set; }
        public string Apellidos { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string? Profesion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Correo { get; set; }
        public List<CursoSimpleDto> Cursos { get; set; } = new List<CursoSimpleDto>();
    }

    public class DocenteCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Profesion { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Correo { get; set; }
    }

    public class DocenteUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Profesion { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Correo { get; set; }
    }

    public class CursoSimpleDto
    {
        public int Id { get; set; }
        public string NombreCurso { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public int HorasSemanal { get; set; }
        public int Ciclo { get; set; }
    }
}
