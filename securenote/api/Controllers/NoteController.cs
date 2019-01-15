using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using data.Notes.Abstractions;
using domain;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<ActionResult<Note>> Index(string id)
        {
            var result = await _noteRepository.GetNote(id);

            return result == null ? (ActionResult<Note>)NotFound() : (ActionResult<Note>)Ok(result);
        }

        /// <summary>
        /// Creates a new note.
        /// </summary>
        /// <returns>The unique identifier for the new note.</returns>
        /// <param name="message">Message.</param>
        /// <response code="201">Created note Id</response>
        /// <response code="400">If the message is null</response>  
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(400)]
        [HttpPost("{message}", Name = "CreateMessage")]
        public async Task<ActionResult<Guid>> Post(string message)
        {
            if (string.IsNullOrEmpty(message)) return BadRequest();

            var note = new Note() { Message = message };

            var createdId = await _noteRepository.CreateNote(note);
            if (createdId != Guid.Empty) return CreatedAtRoute("GetMessage", new { id = createdId }, createdId);

            return BadRequest();
        }
    }
}
