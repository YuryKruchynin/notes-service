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
        if (note.Id == Guid.Empty)
        {
            note.Id = Guid.NewGuid();
        }

        var newNote = new Note
        {
            Id = note.Id,
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
        var updatedNote = new Note
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = note.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };

        // Use AddOrUpdate to atomically update the note
        var result = _notes.AddOrUpdate(
            note.Id,
            // If key doesn't exist, return null (not added)
            _ => null!,
            // If key exists, update it
            (_, existingNote) =>
            {
                updatedNote.CreatedAt = existingNote.CreatedAt;
                return updatedNote;
            });

        // If the result is null, the note didn't exist
        return Task.FromResult(result == null ? null : updatedNote);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var removed = _notes.TryRemove(id, out _);
        return Task.FromResult(removed);
    }
}
