using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CRUDEFCore.DTOs;
using CRUDEFCore.Services;

namespace CRUDEFCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MaintenanceLogController : ControllerBase
    {
        private readonly IMaintenanceLogService _service;

        public MaintenanceLogController(IMaintenanceLogService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllLogsAsync();
            return Ok(result);
        }

        [HttpGet("equipment/{equipmentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByEquipment(int equipmentId)
        {
            var result = await _service.GetLogsByEquipmentIdAsync(equipmentId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MaintenanceLogCreateDto dto)
        {
            var result = await _service.CreateLogAsync(dto);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetByEquipment), new { equipmentId = result.Data!.EquipmentId }, result);
        }
    }
}
