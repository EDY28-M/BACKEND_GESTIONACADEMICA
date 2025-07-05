using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_REST_CURSOSACADEMICOS.Data;
using API_REST_CURSOSACADEMICOS.Models;
using API_REST_CURSOSACADEMICOS.DTOs;

namespace API_REST_CURSOSACADEMICOS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocentesController : ControllerBase
    {
        private readonly GestionAcademicaContext _context;

        public DocentesController(GestionAcademicaContext context)
        {
            _context = context;
        }

        // GET: api/Docentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocenteDto>>> GetDocentes()
        {
            var docentes = await _context.Docentes
                .Include(d => d.Cursos)
                .Select(d => new DocenteDto
                {
                    Id = d.Id,
                    Apellidos = d.Apellidos,
                    Nombres = d.Nombres,
                    Profesion = d.Profesion,
                    FechaNacimiento = d.FechaNacimiento,
                    Correo = d.Correo,
                    Cursos = d.Cursos.Select(c => new CursoSimpleDto
                    {
                        Id = c.Id,
                        NombreCurso = c.NombreCurso,
                        Creditos = c.Creditos,
                        HorasSemanal = c.HorasSemanal,
                        Ciclo = c.Ciclo
                    }).ToList()
                })
                .ToListAsync();

            return Ok(docentes);
        }

        // GET: api/Docentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocenteDto>> GetDocente(int id)
        {
            var docente = await _context.Docentes
                .Include(d => d.Cursos)
                .Where(d => d.Id == id)
                .Select(d => new DocenteDto
                {
                    Id = d.Id,
                    Apellidos = d.Apellidos,
                    Nombres = d.Nombres,
                    Profesion = d.Profesion,
                    FechaNacimiento = d.FechaNacimiento,
                    Correo = d.Correo,
                    Cursos = d.Cursos.Select(c => new CursoSimpleDto
                    {
                        Id = c.Id,
                        NombreCurso = c.NombreCurso,
                        Creditos = c.Creditos,
                        HorasSemanal = c.HorasSemanal,
                        Ciclo = c.Ciclo
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (docente == null)
            {
                return NotFound($"Docente con ID {id} no encontrado");
            }

            return Ok(docente);
        }

        // POST: api/Docentes
        [HttpPost]
        public async Task<ActionResult<DocenteDto>> PostDocente(DocenteCreateDto docenteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el correo ya existe
            if (!string.IsNullOrEmpty(docenteDto.Correo))
            {
                var existeCorreo = await _context.Docentes
                    .AnyAsync(d => d.Correo == docenteDto.Correo);

                if (existeCorreo)
                {
                    return BadRequest("Ya existe un docente con este correo electrónico");
                }
            }

            var docente = new Docente
            {
                Apellidos = docenteDto.Apellidos,
                Nombres = docenteDto.Nombres,
                Profesion = docenteDto.Profesion,
                FechaNacimiento = docenteDto.FechaNacimiento,
                Correo = docenteDto.Correo
            };

            _context.Docentes.Add(docente);
            await _context.SaveChangesAsync();

            var docenteResponse = new DocenteDto
            {
                Id = docente.Id,
                Apellidos = docente.Apellidos,
                Nombres = docente.Nombres,
                Profesion = docente.Profesion,
                FechaNacimiento = docente.FechaNacimiento,
                Correo = docente.Correo,
                Cursos = new List<CursoSimpleDto>()
            };

            return CreatedAtAction(nameof(GetDocente), new { id = docente.Id }, docenteResponse);
        }

        // PUT: api/Docentes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocente(int id, DocenteUpdateDto docenteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null)
            {
                return NotFound($"Docente con ID {id} no encontrado");
            }

            // Verificar si el correo ya existe (excluyendo el docente actual)
            if (!string.IsNullOrEmpty(docenteDto.Correo))
            {
                var existeCorreo = await _context.Docentes
                    .AnyAsync(d => d.Correo == docenteDto.Correo && d.Id != id);

                if (existeCorreo)
                {
                    return BadRequest("Ya existe otro docente con este correo electrónico");
                }
            }

            docente.Apellidos = docenteDto.Apellidos;
            docente.Nombres = docenteDto.Nombres;
            docente.Profesion = docenteDto.Profesion;
            docente.FechaNacimiento = docenteDto.FechaNacimiento;
            docente.Correo = docenteDto.Correo;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocenteExists(id))
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

        // DELETE: api/Docentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocente(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null)
            {
                return NotFound($"Docente con ID {id} no encontrado");
            }

            // Verificar si tiene cursos asignados
            var tieneCursos = await _context.Cursos.AnyAsync(c => c.IdDocente == id);
            if (tieneCursos)
            {
                return BadRequest("No se puede eliminar el docente porque tiene cursos asignados");
            }

            _context.Docentes.Remove(docente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocenteExists(int id)
        {
            return _context.Docentes.Any(e => e.Id == id);
        }
    }
}
