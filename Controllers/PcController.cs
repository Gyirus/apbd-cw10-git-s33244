using Microsoft.AspNetCore.Mvc;
using cw10.DTOs;
using cw10.DTOs;
using cw10.Services;

namespace cw10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  
    public class PcController : ControllerBase
    {
        private readonly IPCService _pcService;

        public PcController(IPCService pcService)
        {
            _pcService = pcService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PcResponseDto>>> GetAllPcs()
        {
            var pcs = await _pcService.GetAllPcsAsync();
            return Ok(pcs);
        }

       
        
        [HttpGet("{id}/components")]
        public async Task<ActionResult<PcWithComponentsDto>> GetPcWithComponents(int id)
        {
            var pc = await _pcService.GetPcWithComponentsAsync(id);
            if (pc == null)
                return NotFound();
            return Ok(pc);
        }

        [HttpPost]
        public async Task<ActionResult<PcResponseDto>> CreatePc([FromBody] PcRequestDto request)
        {
            var created = await _pcService.CreatePcAsync(request);
            return CreatedAtAction(nameof(GetPcWithComponents), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePc(int id, [FromBody] PcRequestDto request)
        {
            var success = await _pcService.UpdatePcAsync(id, request);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePc(int id)
        {
            var success = await _pcService.DeletePcAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}