using System.ComponentModel.DataAnnotations;

namespace API_REST_CURSOSACADEMICOS.DTOs
{
    public class CursoDto
    {
        public int Id { get; set; }
        public string NombreCurso { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public int HorasSemanal { get; set; }
        public int Ciclo { get; set; }
        public int? IdDocente { get; set; }
        public DocenteSimpleDto? Docente { get; set; }
    }

    public class CursoCreateDto
    {
        [Required]
        [StringLength(200)]
        public string NombreCurso { get; set; } = string.Empty;

        [Required]
        [Range(1, 10)]
        public int Creditos { get; set; }

        [Required]
        [Range(1, 40)]
        public int HorasSemanal { get; set; }

        [Required]
        [Range(1, 10)]
        public int Ciclo { get; set; }

        public int? IdDocente { get; set; }
    }

    public class CursoUpdateDto
    {
        [Required]
        [StringLength(200)]
        public string NombreCurso { get; set; } = string.Empty;

        [Required]
        [Range(1, 10)]
        public int Creditos { get; set; }

        [Required]
        [Range(1, 40)]
        public int HorasSemanal { get; set; }

        [Required]
        [Range(1, 10)]
        public int Ciclo { get; set; }

        public int? IdDocente { get; set; }
    }

    public class DocenteSimpleDto
    {
        public int Id { get; set; }
        public string Apellidos { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string? Profesion { get; set; }
    }
}
