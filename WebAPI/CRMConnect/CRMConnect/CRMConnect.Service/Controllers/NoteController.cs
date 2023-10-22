using CRMConnect.CRMConnect.Business.Implementaions;
using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace CRMConnect.CRMConnect.Service.Controllers
{
    [Route("api/Notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService) 
        {
            _noteService = noteService;
        }

        [HttpGet("getAllNotes")]
        public async Task<IActionResult> GetAllNotes() 
        {
            var result = await _noteService.GetAllNotesAsync();
            return Ok(result);
        }

        [HttpGet("getNote/{id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            var result = await _noteService.GetNoteAsync(id);
            return Ok(result);
        }

        [HttpPost("createNote")]
        public async Task<IActionResult> AddNote([FromBody] Note request)
        {
            if(request == null)
            {
                return BadRequest(string.Empty);
            }
            var result = await _noteService.AddNoteAsync(request);
            return Ok(result);
        }


        [HttpDelete("deleteNote/{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var result = await _noteService.DeleteNoteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("updateNote")]
        public async Task<IActionResult> UpdateNote([FromBody] Note request)
        {
            if(request == null)
            {
                return BadRequest(string.Empty);
            }
            var result = await _noteService.UpdateNoteAsync(request);
            return Ok(result);
        }
    }
}
