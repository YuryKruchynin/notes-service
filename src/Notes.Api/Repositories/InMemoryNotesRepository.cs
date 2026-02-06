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

        note.CreatedAt = DateTime.UtcNow;
        note.UpdatedAt = DateTime.UtcNow;

        _notes.TryAdd(note.Id, note);
        return Task.FromResult(note);
    }

    public Task<Note?> UpdateAsync(Note note)
    {
        if (!_notes.ContainsKey(note.Id))
        {
            return Task.FromResult<Note?>(null);
        }

        note.UpdatedAt = DateTime.UtcNow;
        _notes[note.Id] = note;
        return Task.FromResult<Note?>(note);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var removed = _notes.TryRemove(id, out _);
        return Task.FromResult(removed);
    }
}
