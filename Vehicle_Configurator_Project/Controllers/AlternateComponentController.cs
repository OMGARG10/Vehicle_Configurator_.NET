using Microsoft.AspNetCore.Mvc;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Controllers
{
    [ApiController]
    [Route("api/alternatecomponents")]
    public class AlternateComponentController : ControllerBase
    {
        private readonly IAlternateComponentService _service;

        public AlternateComponentController(IAlternateComponentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlternateComponent>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("model/{modelId}")]
        public async Task<ActionResult<List<AlternateComponent>>> GetByModelId(int modelId)
        {
            var items = await _service.GetByModelIdAsync(modelId);
            if (items == null || !items.Any())
                return NotFound($"No alternate components found for model ID {modelId}.");

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlternateComponent>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
                return NotFound($"Alternate component with ID {id} not found.");

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<AlternateComponent>> Create([FromBody] AlternateComponent entity)
        {
            if (entity == null)
                return BadRequest("Alternate component data is required.");

            var created = await _service.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.AltId }, created);
        }
    }
}
