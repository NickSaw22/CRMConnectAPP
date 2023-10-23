using CRMConnect.CRMConnect.Business.Implementaions;
using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CRMConnect.CRMConnect.Service.Controllers
{
    [Route("api/opportunity")]
    [ApiController]
    public class OpportunityController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;
        public OpportunityController(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }

        [HttpGet("getAllOpportunity")]
        public async Task<IActionResult> GetAllOpportunity()
        {
            var response =  await _opportunityService.GetAllOpportunityAsync();
            return Ok(response);
        }

        [HttpGet("getOpportunity/{id}")]
        public async Task<IActionResult> GetAllOpportunity(int id)
        {
            var response = await _opportunityService.GetOpportunityAsync(id);
            return Ok(response);
        }        

        [HttpPost("addOppotunity")]
        public async Task<IActionResult> AddOpportunity([FromBody] Opportunity request)
        {
            if(request == null) return BadRequest(string.Empty);
            return Ok(await _opportunityService.AddOpportunityAsync(request));
        }

        [HttpDelete("deletOpportunity/{id}")]
        public async Task<IActionResult> DeleteOpportunity(int id)
        {
            var response = await _opportunityService.DeleteOpportunityAsync(id);
            if (!response) return NotFound();
            return NoContent();
        }

        [HttpPost("updateOpportunity")]
        public async Task<IActionResult> UpdateOpportunity([FromBody] Opportunity request)
        {
            if(request==null) return BadRequest(string.Empty);
            return Ok(await _opportunityService.UpdateOpportunityAsync(request));
        }

        [HttpGet("getOpportunityStatusWise")]
        public async Task<IActionResult> GetOpportunityStatusWise()
        {
            var response = await _opportunityService.GetOpportunityStatusWiseAsync();
            return Ok(response);
        }
    }
}
