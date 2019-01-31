using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using data.Notes.Abstractions;
using domain;
using Microsoft.AspNetCore.DataProtection;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly IDataProtector _protector;

        public NoteController(INoteRepository noteRepository, IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("note.protector");
            _noteRepository = noteRepository;
        }

        /// <summary>
        /// Returns a note by Id
        /// </summary>
        /// <returns>The note associated with Id</returns>
        /// <param name="id">Note Identifier</param>
        [ProducesResponseType(typeof(Note), 200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<ActionResult<Note>> Index(string id)
        {
            var result = await _noteRepository.GetNote(id);

            if (result != null)
            {
                result.Message = _protector.Unprotect(result.Message);
                return (ActionResult<Note>)Ok(result);
            }
            return (ActionResult<Note>)NotFound();
        }

        /// <summary>
        /// Creates a new note.
        /// </summary>
        /// <returns>The unique identifier for the new note.</returns>
        /// <param name="request">Message.</param>
        /// <response code="201">Created note Id</response>
        /// <response code="400">If the message is null</response>  
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(400)]
        [HttpPost(Name = "CreateMessage")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<Guid>> Post([FromBody] MessageWrapper request)
        {
            if (string.IsNullOrEmpty(request?.Message)) return BadRequest();

            var cipherText = _protector.Protect(request.Message);

            var note = new Note() { Message = cipherText };

            var createdId = await _noteRepository.CreateNote(note);
            if (createdId != Guid.Empty) return CreatedAtRoute("GetMessage", new { id = createdId }, createdId);

            return BadRequest();
        }
    }
}
