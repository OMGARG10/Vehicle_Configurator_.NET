using Microsoft.AspNetCore.Mvc;
using Vehicle_Configurator_Project.Models;
using Vehicle_Configurator_Project.Services;

namespace Vehicle_Configurator_Project.Controllers
{
    [ApiController]
    [Route("api/model")]
    public class ModelController : ControllerBase
    {
        private readonly IModelService modelService;

        public ModelController(IModelService modelService)
        {
            this.modelService = modelService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Model>>> GetAllModels()
        {
            var models = await modelService.GetAllModelsAsync();
            return Ok(models);
        }
    }
}
