using Microsoft.AspNetCore.Mvc;
using Notes.Api.DTOs;
using Notes.Api.Services;

namespace Notes.Api.Controllers;

/// <summary>
/// Controller for managing notes with full CRUD operations
/// </summary>
[ApiController]
[Route("api/notes")]
public class NotesController : ControllerBase
{
    private readonly INotesService _notesService;

    /// <summary>
    /// Initializes a new instance of the NotesController
    /// </summary>
    /// <param name="notesService">The notes service</param>
    public NotesController(INotesService notesService)
    {
        _notesService = notesService;
    }

    /// <summary>
    /// Gets all notes
    /// </summary>
    /// <returns>A list of all notes</returns>
    /// <response code="200">Returns the list of notes</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NoteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetAllNotes()
    {
        try
        {
            var notes = await _notesService.GetAllNotesAsync();
            return Ok(notes);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "An error occurred while retrieving notes.", error = ex.Message });
        }
    }

    /// <summary>
    /// Gets a specific note by ID
    /// </summary>
    /// <param name="id">The note ID</param>
    /// <returns>The requested note</returns>
    /// <response code="200">Returns the requested note</response>
    /// <response code="404">If the note is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(NoteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<NoteDto>> GetNoteById(Guid id)
    {
        try
        {
            var note = await _notesService.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound(new { message = $"Note with ID {id} not found." });
            }
            return Ok(note);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "An error occurred while retrieving the note.", error = ex.Message });
        }
    }

    /// <summary>
    /// Creates a new note
    /// </summary>
    /// <param name="createNoteDto">The note data</param>
    /// <returns>The created note</returns>
    /// <response code="201">Returns the newly created note</response>
    /// <response code="400">If the note data is invalid</response>
    [HttpPost]
    [ProducesResponseType(typeof(NoteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<NoteDto>> CreateNote([FromBody] CreateNoteDto createNoteDto)
    {
        try
        {
            var note = await _notesService.CreateNoteAsync(createNoteDto);
            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "An error occurred while creating the note.", error = ex.Message });
        }
    }

    /// <summary>
    /// Updates an existing note
    /// </summary>
    /// <param name="id">The note ID</param>
    /// <param name="updateNoteDto">The updated note data</param>
    /// <returns>The updated note</returns>
    /// <response code="200">Returns the updated note</response>
    /// <response code="404">If the note is not found</response>
    /// <response code="400">If the note data is invalid</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(NoteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<NoteDto>> UpdateNote(Guid id, [FromBody] UpdateNoteDto updateNoteDto)
    {
        try
        {
            var note = await _notesService.UpdateNoteAsync(id, updateNoteDto);
            if (note == null)
            {
                return NotFound(new { message = $"Note with ID {id} not found." });
            }
            return Ok(note);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "An error occurred while updating the note.", error = ex.Message });
        }
    }

    /// <summary>
    /// Deletes a note
    /// </summary>
    /// <param name="id">The note ID</param>
    /// <returns>No content</returns>
    /// <response code="204">If the note was successfully deleted</response>
    /// <response code="404">If the note is not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteNote(Guid id)
    {
        try
        {
            var success = await _notesService.DeleteNoteAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Note with ID {id} not found." });
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "An error occurred while deleting the note.", error = ex.Message });
        }
    }
}
