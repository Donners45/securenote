using System;
using System.Net;
using System.Threading.Tasks;
using data.Notes.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            this._noteRepository = noteRepository;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<domain.Notes.Note>> Index(string id)
        {
            var result = await _noteRepository.GetNote(id);

            if (result != null) return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Creates a new note.
        /// </summary>
        /// <returns>The unique identifier for the new note.</returns>
        /// <param name="message">Message.</param>
        /// <response code="201">Created note Id</response>
        /// <response code="400">If the message is null</response>    
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("{message}")]
        public async Task<ActionResult<Guid>> Post(string message)
        {
            if (string.IsNullOrEmpty(message)) return BadRequest();

            var createdId = await _noteRepository.CreateNote(new domain.Notes.Note() { Message = message });

            if (createdId != Guid.Empty) return Created("/", createdId);
            return BadRequest();
        }
    }
}
