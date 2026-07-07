using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CRUDEFCore.DTOs;
using CRUDEFCore.Services;

namespace CRUDEFCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _service;

        public EquipmentController(IEquipmentService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllEquipmentsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetEquipmentByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var result = await _service.SearchEquipmentByNameAsync(keyword);
            return Ok(result);
        }

        [HttpGet("department/{departmentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            var result = await _service.GetEquipmentsByDepartmentAsync(departmentId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EquipmentCreateDto dto)
        {
            var result = await _service.CreateEquipmentAsync(dto);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EquipmentUpdateDto dto)
        {
            var result = await _service.UpdateEquipmentAsync(id, dto);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteEquipmentAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign([FromBody] AssignDto dto)
        {
            var result = await _service.AssignEquipmentToEmployeeAsync(dto.EquipmentId, dto.EmployeeId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
