using Microsoft.AspNetCore.Mvc;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Controllers
{
    [ApiController]
    [Route("api/segments")]
    public class SegmentController : ControllerBase
    {
        private readonly ISegmentService segmentService;

        public SegmentController(ISegmentService segmentService)
        {
            this.segmentService = segmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Segment>>> GetAllSegments()
        {
            var segments = await segmentService.GetAllSegmentsAsync();
            return Ok(segments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Segment>> GetSegmentById(int id)
        {
            try
            {
                var segment = await segmentService.GetSegmentByIdAsync(id);
                return Ok(segment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Segment>> GetSegmentByName(string name)
        {
            try
            {
                var segment = await segmentService.GetSegmentByNameAsync(name);
                return Ok(segment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Segment>> CreateSegment(Segment segment)
        {
            var createdSegment = await segmentService.CreateSegmentAsync(segment);
            return CreatedAtAction(nameof(GetSegmentById), new { id = createdSegment.SegId }, createdSegment);
        }
    }
}
