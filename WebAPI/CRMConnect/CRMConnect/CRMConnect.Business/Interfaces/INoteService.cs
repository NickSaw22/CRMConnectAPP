using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Business.Interfaces
{
    public interface INoteService
    {
        Task<List<Note>> GetAllNotesAsync();
        Task<Note> GetNoteAsync(int id);
        Task<bool> DeleteNoteAsync(int id);
        Task<Note> AddNoteAsync(Note Note);
        Task<bool> UpdateNoteAsync(Note Note);
    }
}
