using CRMConnect.CRMConnect.Business.Implementaions;
using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace CRMConnect.CRMConnect.Service.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService) 
        {
            _contactService = contactService;
        }

        [HttpGet("getAllContacts")]
        public async Task<IActionResult> GetAllContacts() 
        {
            var result = await _contactService.GetAllContactsAsync();
            return Ok(result);
        }

        [HttpGet("getContact/{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var result = await _contactService.GetContactAsync(id);
            return Ok(result);
        }

        [HttpPost("createContact")]
        public async Task<IActionResult> AddContact([FromBody] Contact request)
        {
            if(request == null)
            {
                return BadRequest(string.Empty);
            }
            var result = await _contactService.AddContactAsync(request);
            return Ok(result);
        }


        [HttpDelete("deleteContact/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _contactService.DeleteContactAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("updateContact")]
        public async Task<IActionResult> UpdateContact([FromBody] Contact request)
        {
            if(request == null)
            {
                return BadRequest(string.Empty);
            }
            var result = await _contactService.UpdateContactAsync(request);
            return Ok(result);
        }
    }
}
