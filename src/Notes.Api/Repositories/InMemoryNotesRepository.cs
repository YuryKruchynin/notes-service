using System.Collections.Concurrent;
using Notes.Api.Models;

namespace Notes.Api.Repositories;

public class InMemoryNotesRepository : INotesRepository
{
    private readonly ConcurrentDictionary<Guid, Note> _notes = new();

    public Task<IEnumerable<Note>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Note>>(_notes.Values.ToList());
    }

    public Task<Note?> GetByIdAsync(Guid id)
    {
        _notes.TryGetValue(id, out var note);
        return Task.FromResult(note);
    }

    public Task<Note> CreateAsync(Note note)
    {
        var id = note.Id == Guid.Empty ? Guid.NewGuid() : note.Id;

        var newNote = new Note
        {
            Id = id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        if (!_notes.TryAdd(newNote.Id, newNote))
        {
            throw new InvalidOperationException($"A note with ID {newNote.Id} already exists.");
        }

        return Task.FromResult(newNote);
    }

    public Task<Note?> UpdateAsync(Note note)
    {
        Note? result = null;
        
        _notes.AddOrUpdate(
            note.Id,
            // If key doesn't exist, don't add anything and return null
            key => 
            {
                result = null;
                return null!;
            },
            // If key exists, update it
            (key, existingNote) =>
            {
                var updatedNote = new Note
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content,
                    CreatedAt = existingNote.CreatedAt,
                    UpdatedAt = DateTime.UtcNow
                };
                result = updatedNote;
                return updatedNote;
            });

        // Clean up if we accidentally added a null entry
        if (result == null)
        {
            _notes.TryRemove(note.Id, out _);
        }

        return Task.FromResult(result);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var removed = _notes.TryRemove(id, out _);
        return Task.FromResult(removed);
    }
}
