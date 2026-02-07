using System.Collections.Concurrent;
using Notes.Api.Models;

namespace Notes.Api.Repositories;

public class InMemoryNotesRepository : INotesRepository
{
    private readonly ConcurrentDictionary<Guid, Note> _notes = new();
    private readonly object _updateLock = new();

    public InMemoryNotesRepository()
    {
        // Initialize with sample seed data
        SeedData();
    }

    private void SeedData()
    {
        var baseTime = DateTime.UtcNow;
        
        var seedNotes = new[]
        {
            new Note
            {
                Id = Guid.NewGuid(),
                Title = "Welcome Note",
                Content = "Welcome to the Notes Service! This is a sample note to help you get started. You can create, read, update, and delete notes using this API. Feel free to explore the Swagger UI to test all available endpoints.",
                CreatedAt = baseTime.AddDays(-5),
                UpdatedAt = baseTime.AddDays(-5)
            },
            new Note
            {
                Id = Guid.NewGuid(),
                Title = "Meeting Notes",
                Content = "Team Meeting - Q1 Planning\n\nAttendees: John, Sarah, Mike\nDate: " + baseTime.AddDays(-3).ToString("yyyy-MM-dd") + "\n\nAgenda:\n1. Review Q4 results\n2. Set Q1 objectives\n3. Resource allocation\n\nAction Items:\n- John: Prepare budget proposal\n- Sarah: Update project timeline\n- Mike: Schedule follow-up meeting",
                CreatedAt = baseTime.AddDays(-3),
                UpdatedAt = baseTime.AddDays(-2)
            },
            new Note
            {
                Id = Guid.NewGuid(),
                Title = "Todo List",
                Content = "Daily Tasks:\n\n✓ Review pull requests\n✓ Update documentation\n- Implement new feature\n- Write unit tests\n- Deploy to staging\n- Code review session at 3 PM\n\nRemember to backup database before deployment!",
                CreatedAt = baseTime.AddDays(-2),
                UpdatedAt = baseTime.AddDays(-1)
            },
            new Note
            {
                Id = Guid.NewGuid(),
                Title = "Ideas",
                Content = "Product Brainstorming Session\n\nNew Features:\n- Dark mode support\n- Export notes to PDF\n- Markdown formatting\n- Tags and categories\n- Search functionality\n- Note sharing capabilities\n\nNext steps: Prioritize features and create user stories",
                CreatedAt = baseTime.AddDays(-1),
                UpdatedAt = baseTime.AddDays(-1)
            },
            new Note
            {
                Id = Guid.NewGuid(),
                Title = "Quick Reminder",
                Content = "Don't forget: Team lunch tomorrow at 12:30 PM at the Italian restaurant downtown!",
                CreatedAt = baseTime.AddHours(-6),
                UpdatedAt = baseTime.AddHours(-6)
            }
        };

        foreach (var note in seedNotes)
        {
            _notes.TryAdd(note.Id, note);
        }
    }

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
        lock (_updateLock)
        {
            // Check if the note exists
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

            _notes[note.Id] = updatedNote;
            return Task.FromResult<Note?>(updatedNote);
        }
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var removed = _notes.TryRemove(id, out _);
        return Task.FromResult(removed);
    }
}
