using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Controllers
{
    [ApiController]
    [Route("api/models")]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly ISegmentService _segmentService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IVehicleDetailService _vehicleDetailService;

        public ModelController(
            IModelService modelService,
            ISegmentService segmentService,
            IManufacturerService manufacturerService,
            IVehicleDetailService vehicleDetailService)
        {
            _modelService = modelService;
            _segmentService = segmentService;
            _manufacturerService = manufacturerService;
            _vehicleDetailService = vehicleDetailService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Model>>> GetAllModels()
        {
            var models = await _modelService.GetAllModelsAsync();
            return Ok(models);
        }

        [HttpPost]
        public async Task<ActionResult<Model>> AddModel([FromBody] Model model)
        {
            var created = await _modelService.CreateModelAsync(model);
            return CreatedAtAction(nameof(GetModelById), new { modelId = created.ModelId }, created);
        }

        [HttpGet("{modelId:int}")]
        public async Task<ActionResult<Model>> GetModelById(int modelId)
        {
            var model = await _modelService.GetModelByIdAsync(modelId);
            if (model == null) return NotFound();
            return Ok(model);
        }

        [HttpGet("by-segment/{segmentId:int}")]
        public async Task<ActionResult<List<Model>>> GetModelsBySegment(int segmentId)
        {
            var segment = await _segmentService.GetSegmentByIdAsync(segmentId);
            if (segment == null) return NotFound();

            var models = await _modelService.GetModelsBySegmentAsync(segment);
            return Ok(models);
        }

        [HttpGet("by-segment/{segmentId:int}/manufacturer/{manufacturerId:int}")]
        public async Task<ActionResult<List<Model>>> GetModelsBySegmentAndManufacturer(int segmentId, int manufacturerId)
        {
            var segment = await _segmentService.GetSegmentByIdAsync(segmentId);
            if (segment == null) return NotFound();

            var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(manufacturerId);
            if (manufacturer == null) return NotFound();

            var models = await _modelService.GetModelsByManufacturerAndSegmentAsync(manufacturer, segment);
            return Ok(models);
        }

        [HttpGet("default/{modelId:int}")]
        public async Task<ActionResult<Model>> GetModelWithDefaults(int modelId)
        {
            var model = await _modelService.GetModelWithDefaultComponentsAsync(modelId);
            if (model == null) return NotFound();
            return Ok(model);
        }

        [HttpGet("configurable/{modelId:int}")]
        public async Task<ActionResult<List<VehicleDetail>>> GetConfigurableComponents(int modelId)
        {
            // Note the char 'Y' instead of string "Y" to avoid type errors
            var components = await _vehicleDetailService.GetByModelIdAndIsConfigurableAsync(modelId, 'Y');
            return Ok(components);
        }

        [HttpGet("components/{modelId:int}")]
        public async Task<ActionResult<List<VehicleDetail>>> GetAllComponents(int modelId)
        {
            var components = await _modelService.GetComponentsByModelIdAsync(modelId);
            return Ok(components);
        }

        [HttpGet("alternate-components/{modelId:int}")]
        public async Task<ActionResult<Dictionary<char, Dictionary<int, List<AlternateComponent>>>>> GetAlternateComponentsGrouped(int modelId)
        {
            var grouped = await _modelService.GetGroupedAlternatesByModelAsync(modelId);
            return Ok(grouped);
        }

        [HttpGet("price/{modelId:int}")]
        public async Task<ActionResult<decimal>> GetBasePrice(int modelId)
        {
            var price = await _modelService.GetBasePriceByModelIdAsync(modelId);
            if (price == null) return NotFound();
            return Ok(price);
        }
    }
}
