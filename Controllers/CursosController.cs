using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_REST_CURSOSACADEMICOS.Data;
using API_REST_CURSOSACADEMICOS.Models;
using API_REST_CURSOSACADEMICOS.DTOs;

namespace API_REST_CURSOSACADEMICOS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        private readonly GestionAcademicaContext _context;

        public CursosController(GestionAcademicaContext context)
        {
            _context = context;
        }

        // GET: api/Cursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoDto>>> GetCursos()
        {
            var cursos = await _context.Cursos
                .Include(c => c.Docente)
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    NombreCurso = c.NombreCurso,
                    Creditos = c.Creditos,
                    HorasSemanal = c.HorasSemanal,
                    Ciclo = c.Ciclo,
                    IdDocente = c.IdDocente,
                    Docente = c.Docente != null ? new DocenteSimpleDto
                    {
                        Id = c.Docente.Id,
                        Apellidos = c.Docente.Apellidos,
                        Nombres = c.Docente.Nombres,
                        Profesion = c.Docente.Profesion
                    } : null
                })
                .ToListAsync();

            return Ok(cursos);
        }

        // GET: api/Cursos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDto>> GetCurso(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Docente)
                .Where(c => c.Id == id)
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    NombreCurso = c.NombreCurso,
                    Creditos = c.Creditos,
                    HorasSemanal = c.HorasSemanal,
                    Ciclo = c.Ciclo,
                    IdDocente = c.IdDocente,
                    Docente = c.Docente != null ? new DocenteSimpleDto
                    {
                        Id = c.Docente.Id,
                        Apellidos = c.Docente.Apellidos,
                        Nombres = c.Docente.Nombres,
                        Profesion = c.Docente.Profesion
                    } : null
                })
                .FirstOrDefaultAsync();

            if (curso == null)
            {
                return NotFound($"Curso con ID {id} no encontrado");
            }

            return Ok(curso);
        }

        // GET: api/Cursos/PorDocente/5
        [HttpGet("PorDocente/{docenteId}")]
        public async Task<ActionResult<IEnumerable<CursoDto>>> GetCursosPorDocente(int docenteId)
        {
            var docenteExiste = await _context.Docentes.AnyAsync(d => d.Id == docenteId);
            if (!docenteExiste)
            {
                return NotFound($"Docente con ID {docenteId} no encontrado");
            }

            var cursos = await _context.Cursos
                .Include(c => c.Docente)
                .Where(c => c.IdDocente == docenteId)
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    NombreCurso = c.NombreCurso,
                    Creditos = c.Creditos,
                    HorasSemanal = c.HorasSemanal,
                    Ciclo = c.Ciclo,
                    IdDocente = c.IdDocente,
                    Docente = c.Docente != null ? new DocenteSimpleDto
                    {
                        Id = c.Docente.Id,
                        Apellidos = c.Docente.Apellidos,
                        Nombres = c.Docente.Nombres,
                        Profesion = c.Docente.Profesion
                    } : null
                })
                .ToListAsync();

            return Ok(cursos);
        }

        // GET: api/Cursos/PorCiclo/1
        [HttpGet("PorCiclo/{ciclo}")]
        public async Task<ActionResult<IEnumerable<CursoDto>>> GetCursosPorCiclo(int ciclo)
        {
            var cursos = await _context.Cursos
                .Include(c => c.Docente)
                .Where(c => c.Ciclo == ciclo)
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    NombreCurso = c.NombreCurso,
                    Creditos = c.Creditos,
                    HorasSemanal = c.HorasSemanal,
                    Ciclo = c.Ciclo,
                    IdDocente = c.IdDocente,
                    Docente = c.Docente != null ? new DocenteSimpleDto
                    {
                        Id = c.Docente.Id,
                        Apellidos = c.Docente.Apellidos,
                        Nombres = c.Docente.Nombres,
                        Profesion = c.Docente.Profesion
                    } : null
                })
                .ToListAsync();

            return Ok(cursos);
        }

        // POST: api/Cursos
        [HttpPost]
        public async Task<ActionResult<CursoDto>> PostCurso(CursoCreateDto cursoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el docente existe
            if (cursoDto.IdDocente.HasValue)
            {
                var docenteExiste = await _context.Docentes.AnyAsync(d => d.Id == cursoDto.IdDocente);
                if (!docenteExiste)
                {
                    return BadRequest($"Docente con ID {cursoDto.IdDocente} no encontrado");
                }
            }

            var curso = new Curso
            {
                NombreCurso = cursoDto.NombreCurso,
                Creditos = cursoDto.Creditos,
                HorasSemanal = cursoDto.HorasSemanal,
                Ciclo = cursoDto.Ciclo,
                IdDocente = cursoDto.IdDocente
            };

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            // Cargar el docente para la respuesta
            await _context.Entry(curso)
                .Reference(c => c.Docente)
                .LoadAsync();

            var cursoResponse = new CursoDto
            {
                Id = curso.Id,
                NombreCurso = curso.NombreCurso,
                Creditos = curso.Creditos,
                HorasSemanal = curso.HorasSemanal,
                Ciclo = curso.Ciclo,
                IdDocente = curso.IdDocente,
                Docente = curso.Docente != null ? new DocenteSimpleDto
                {
                    Id = curso.Docente.Id,
                    Apellidos = curso.Docente.Apellidos,
                    Nombres = curso.Docente.Nombres,
                    Profesion = curso.Docente.Profesion
                } : null
            };

            return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, cursoResponse);
        }

        // PUT: api/Cursos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, CursoUpdateDto cursoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound($"Curso con ID {id} no encontrado");
            }

            // Verificar si el docente existe
            if (cursoDto.IdDocente.HasValue)
            {
                var docenteExiste = await _context.Docentes.AnyAsync(d => d.Id == cursoDto.IdDocente);
                if (!docenteExiste)
                {
                    return BadRequest($"Docente con ID {cursoDto.IdDocente} no encontrado");
                }
            }

            curso.NombreCurso = cursoDto.NombreCurso;
            curso.Creditos = cursoDto.Creditos;
            curso.HorasSemanal = cursoDto.HorasSemanal;
            curso.Ciclo = cursoDto.Ciclo;
            curso.IdDocente = cursoDto.IdDocente;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Cursos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound($"Curso con ID {id} no encontrado");
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
