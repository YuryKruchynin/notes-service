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
        // Check if the note exists before updating
        if (!_notes.TryGetValue(note.Id, out var existingNote))
        {
            return Task.FromResult<Note?>(null);
        }

        var updatedNote = new Note
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = existingNote.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };

        // Use indexer to update (safe because we verified existence)
        _notes[note.Id] = updatedNote;
        return Task.FromResult<Note?>(updatedNote);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var removed = _notes.TryRemove(id, out _);
        return Task.FromResult(removed);
    }
}
