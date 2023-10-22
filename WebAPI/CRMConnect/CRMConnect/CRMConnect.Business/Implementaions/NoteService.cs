using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.Interfaces;

namespace CRMConnect.CRMConnect.Business.Implementaions
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public Task<Note> AddNoteAsync(Note Note)
        {
            return _noteRepository.AddNoteDataAsync(Note);
        }

        public Task<bool> DeleteNoteAsync(int id)
        {
            return _noteRepository.DeleteNodeDataAsync(id);
        }

        public Task<List<Note>> GetAllNotesAsync()
        {
            return _noteRepository.GetAllNotesDataAsync();
        }

        public async Task<Note> GetNoteAsync(int id)
        {
            return await _noteRepository.GetNoteByIdDataAsync(id);
        }

        public async Task<bool> UpdateNoteAsync(Note Note)
        {
            return await _noteRepository.UpdateNoteDataAsync(Note);
        }
    }
}
