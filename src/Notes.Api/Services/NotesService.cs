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
        try
        {
            var notes = await _repository.GetAllAsync();
            return notes.Select(MapToDto);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve notes from repository.", ex);
        }
    }

    public async Task<NoteDto?> GetNoteByIdAsync(Guid id)
    {
        try
        {
            var note = await _repository.GetByIdAsync(id);
            return note == null ? null : MapToDto(note);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to retrieve note with ID {id} from repository.", ex);
        }
    }

    public async Task<NoteDto> CreateNoteAsync(CreateNoteDto dto)
    {
        try
        {
            var note = new Note
            {
                Title = dto.Title,
                Content = dto.Content
            };

            var createdNote = await _repository.CreateAsync(note);
            return MapToDto(createdNote);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to create note in repository.", ex);
        }
    }

    public async Task<NoteDto?> UpdateNoteAsync(Guid id, UpdateNoteDto dto)
    {
        try
        {
            var note = new Note
            {
                Id = id,
                Title = dto.Title,
                Content = dto.Content
            };

            var updatedNote = await _repository.UpdateAsync(note);
            return updatedNote == null ? null : MapToDto(updatedNote);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to update note with ID {id} in repository.", ex);
        }
    }

    public async Task<bool> DeleteNoteAsync(Guid id)
    {
        try
        {
            return await _repository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to delete note with ID {id} from repository.", ex);
        }
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
