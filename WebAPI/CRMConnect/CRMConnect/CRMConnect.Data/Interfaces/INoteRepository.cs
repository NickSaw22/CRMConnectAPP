using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Data.Interfaces
{
    public interface INoteRepository
    {
        Task<List<Note>> GetAllNotesDataAsync();
        Task<Note> GetNoteByIdDataAsync(int id);
        Task<Note> AddNoteDataAsync(Note note);
        Task<bool> DeleteNodeDataAsync(int id);
        Task<bool> UpdateNoteDataAsync(Note note);
    }
}
