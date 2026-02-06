using Notes.Api.DTOs;
using Notes.Api.Models;
using Notes.Api.Repositories;

namespace Notes.Api.Services;

public class NotesService : INotesService
{
    private readonly INotesRepository _repository;

    public NotesService(INotesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<NoteDto>> GetAllNotesAsync()
    {
        var notes = await _repository.GetAllAsync();
        return notes.Select(MapToDto);
    }

    public async Task<NoteDto?> GetNoteByIdAsync(Guid id)
    {
        var note = await _repository.GetByIdAsync(id);
        return note == null ? null : MapToDto(note);
    }

    public async Task<NoteDto> CreateNoteAsync(CreateNoteDto dto)
    {
        var note = new Note
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createdNote = await _repository.CreateAsync(note);
        return MapToDto(createdNote);
    }

    public async Task<NoteDto?> UpdateNoteAsync(Guid id, UpdateNoteDto dto)
    {
        // Check if note exists first
        var existingNote = await _repository.GetByIdAsync(id);
        if (existingNote == null)
        {
            return null;
        }

        var note = new Note
        {
            Id = id,
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = existingNote.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };

        var updatedNote = await _repository.UpdateAsync(note);
        return updatedNote == null ? null : MapToDto(updatedNote);
    }

    public async Task<bool> DeleteNoteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static NoteDto MapToDto(Note note)
    {
        return new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = note.CreatedAt,
            UpdatedAt = note.UpdatedAt
        };
    }
}
