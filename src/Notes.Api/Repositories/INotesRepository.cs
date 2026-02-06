using Notes.Api.Models;

namespace Notes.Api.Repositories;

public interface INotesRepository
{
    Task<IEnumerable<Note>> GetAllAsync();
    Task<Note?> GetByIdAsync(Guid id);
    Task<Note> CreateAsync(Note note);
    Task<Note?> UpdateAsync(Note note);
    Task<bool> DeleteAsync(Guid id);
}
