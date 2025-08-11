using Microsoft.AspNetCore.Mvc;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Controllers
{
    [ApiController]
    [Route("api/components")]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService componentService;

        public ComponentController(IComponentService componentService)
        {
            this.componentService = componentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Component>>> GetAll()
        {
            var components = await componentService.GetAllComponentsAsync();
            return Ok(components);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Component>> GetById(int id)
        {
            var component = await componentService.GetComponentByIdAsync(id);
            if (component == null)
                return NotFound();

            return Ok(component);
        }

        [HttpGet("name/{compName}")]
        public async Task<ActionResult<Component>> GetByName(string compName)
        {
            var component = await componentService.GetComponentByNameAsync(compName);
            if (component == null)
                return NotFound();

            return Ok(component);
        }

        [HttpPost]
        public async Task<ActionResult<Component>> Create(Component component)
        {
            var createdComponent = await componentService.CreateComponentAsync(component);
            return CreatedAtAction(nameof(GetById), new { id = createdComponent.CompId }, createdComponent);
        }
    }
}
