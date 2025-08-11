using Microsoft.AspNetCore.Mvc;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleDetailController : ControllerBase
    {
        private readonly IVehicleDetailService _service;

        public VehicleDetailController(IVehicleDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehicleDetail>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("model/{modelId}")]
        public async Task<ActionResult<List<VehicleDetail>>> GetByModelId(int modelId)
        {
            var items = await _service.GetByModelIdAsync(modelId);
            if (items == null || !items.Any())
                return NotFound($"No vehicle details found for model ID {modelId}.");

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDetail>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
                return NotFound($"Vehicle detail with ID {id} not found.");

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDetail>> Create([FromBody] VehicleDetail vehicleDetail)
        {
            if (vehicleDetail == null)
                return BadRequest("Vehicle detail data is required.");

            var created = await _service.CreateAsync(vehicleDetail);
            return CreatedAtAction(nameof(GetById), new { id = created.ConfigId }, created);
        }
    }
}
