using Notes.Api.DTOs;

namespace Notes.Api.Services;

public interface INotesService
{
    Task<IEnumerable<NoteDto>> GetAllNotesAsync();
    Task<NoteDto?> GetNoteByIdAsync(Guid id);
    Task<NoteDto> CreateNoteAsync(CreateNoteDto dto);
    Task<NoteDto?> UpdateNoteAsync(Guid id, UpdateNoteDto dto);
    Task<bool> DeleteNoteAsync(Guid id);
}
