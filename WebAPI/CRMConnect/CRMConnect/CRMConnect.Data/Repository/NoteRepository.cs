using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.DataAccess;
using CRMConnect.CRMConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRMConnect.CRMConnect.Data.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;
        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;            
        }

        public async Task<Note> AddNoteDataAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<bool> DeleteNodeDataAsync(int id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n=>n.Id== id);
            if(note==null) return false;
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Note>> GetAllNotesDataAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note> GetNoteByIdDataAsync(int id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n=>n.Id == id);
            return note;
        }

        public async Task<bool> UpdateNoteDataAsync(Note note)
        {
            var existingNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == note.Id);
            if(existingNote==null) return false;
            existingNote.NoteTitle = note.NoteTitle;
            existingNote.CreatedOn = note.CreatedOn;
            existingNote.CreatedBy = note.CreatedBy;
            existingNote.AssociatedEntityId = note.AssociatedEntityId;
            existingNote.AssociatedEntityType = note.AssociatedEntityType;
            _context.Notes.Update(existingNote);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
