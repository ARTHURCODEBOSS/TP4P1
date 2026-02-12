using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP4P1.Models.EntityFramework;

namespace TP4P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotationsController : ControllerBase
    {
        private readonly FilmRatingsDBContext _context;

        public NotationsController(FilmRatingsDBContext context)
        {
            _context = context;
        }

        // GET: api/Notations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notation>>> GetNotations()
        {
            return await _context.Notations.ToListAsync();
        }

        // GET: api/Notations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notation>> GetNotation(int id)
        {
            var notation = await _context.Notations.FindAsync(id);

            if (notation == null)
            {
                return NotFound();
            }

            return notation;
        }

        // PUT: api/Notations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotation(int id, Notation notation)
        {
            if (id != notation.UtilisateurId)
            {
                return BadRequest();
            }

            _context.Entry(notation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotationExists(id))
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

        // POST: api/Notations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notation>> PostNotation(Notation notation)
        {
            _context.Notations.Add(notation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NotationExists(notation.UtilisateurId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNotation", new { id = notation.UtilisateurId }, notation);
        }

        // DELETE: api/Notations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotation(int id)
        {
            var notation = await _context.Notations.FindAsync(id);
            if (notation == null)
            {
                return NotFound();
            }

            _context.Notations.Remove(notation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotationExists(int id)
        {
            return _context.Notations.Any(e => e.UtilisateurId == id);
        }
    }
}
