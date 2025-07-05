using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_CURSOSACADEMICOS.Models
{
    [Table("Curso")]
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Column("curso")]
        public string NombreCurso { get; set; } = string.Empty;

        [Required]
        public int Creditos { get; set; }

        [Required]
        public int HorasSemanal { get; set; }

        [Required]
        public int Ciclo { get; set; }

        // Foreign Key
        [Column("idDocente")]
        public int? IdDocente { get; set; }

        // Navigation Property
        [ForeignKey("IdDocente")]
        public virtual Docente? Docente { get; set; }
    }
}
