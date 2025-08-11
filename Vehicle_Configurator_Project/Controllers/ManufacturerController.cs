using Microsoft.AspNetCore.Mvc;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Controllers
{
    [ApiController]
    [Route("api/manufacturers")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService service;

        public ManufacturerController(IManufacturerService service)
        {
            this.service = service;
        }

        [HttpGet("seg/{segId}")]
        public async Task<ActionResult<List<Manufacturer>>> GetManufacturersBySegment(int segId)
        {
            var manufacturers = await service.GetBySegmentIdAsync(segId);

            if (manufacturers == null || manufacturers.Count == 0)
                return NotFound();

            return Ok(manufacturers);
        }
    }
}
