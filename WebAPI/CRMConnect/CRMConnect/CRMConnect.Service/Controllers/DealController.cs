using CRMConnect.CRMConnect.Business.Implementaions;
using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace CRMConnect.CRMConnect.Service.Controllers
{
    [Route("api/Deals")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IDealService _dealService;
        public DealController(IDealService dealService) 
        {
            _dealService = dealService;
        }

        [HttpGet("getAllDeals")]
        public async Task<IActionResult> GetAllDeals() 
        {
            var result = await _dealService.GetAllDealsAsync();
            return Ok(result);
        }

        [HttpGet("getDeal/{id}")]
        public async Task<IActionResult> GetDeal(int id)
        {
            var result = await _dealService.GetDealAsync(id);
            return Ok(result);
        }

        [HttpPost("createDeal")]
        public async Task<IActionResult> AddDeal([FromBody] Deal request)
        {
            if(request == null)
            {
                return BadRequest(string.Empty);
            }
            var result = await _dealService.AddDealAsync(request);
            return Ok(result);
        }


        [HttpDelete("deleteDeal/{id}")]
        public async Task<IActionResult> DeleteDeal(int id)
        {
            var result = await _dealService.DeleteDealAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("updateDeal")]
        public async Task<IActionResult> UpdateDeal([FromBody] Deal request)
        {
            if(request == null)
            {
                return BadRequest(string.Empty);
            }
            var result = await _dealService.UpdateDealAsync(request);
            return Ok(result);
        }
    }
}
