﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using data.Notes;
using data.Notes.Abstractions;
using domain;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly ILogger<NoteController> _logger;

        public NoteController(INoteRepository noteRepository, ILogger<NoteController> logger)
        {
            this._noteRepository = noteRepository;
            this._logger = logger;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<ActionResult<Note>> Index(string id)
        {
            var result = await _noteRepository.GetNote(id);

            if (result == null) return NotFound();

            return Ok(result);

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
