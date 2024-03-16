using AlmoxarifadoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlmoxarifadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly AlmoxarifadoAPIContext _context;

        public EmailController(AlmoxarifadoAPIContext context)
        {
            _context = context;
        }

        // GET: api/Email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Email>>> GetEmails()
        {
            return await _context.Emails.ToListAsync();
        }

        // POST: api/Email
        [HttpPost]
        public async Task<ActionResult<Email>> PostEmail(Email email)
        {
            _context.Emails.Add(email);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmails), new { id = email.idEmail }, email);
        }

        // PATCH: api/Email/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEmail(int id, Email emailAtualizado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emailAtualizado.idEmail)
            {
                return BadRequest("O ID fornecido no corpo da solicitação não corresponde ao ID fornecido no URL.");
            }

            var emailExistente = await _context.Emails.FindAsync(id);

            if (emailExistente == null)
            {
                return NotFound();
            }

            // Atualiza apenas o email do registro encontrado
            emailExistente.EmailUsuario = emailAtualizado.EmailUsuario;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailExists(id))
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

        private bool EmailExists(int idEmail)
        {
            return _context.Emails.Any(e => e.idEmail == idEmail);
        }
    }
}
