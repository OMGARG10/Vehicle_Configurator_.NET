using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;
using Vehicle_Configurator_Project.Services;

namespace Vehicle_Configurator_Project.Controllers
{
    [ApiController]
    [Route("api/invoiceheader")]
    public class InvoiceHeaderController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceHeaderController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceWrapper wrapper)
        {
            try
            {
                var createdInvoice = await _invoiceService.CreateInvoiceAsync(wrapper);
                return CreatedAtAction(nameof(GetInvoiceById), new { id = createdInvoice.InvId }, createdInvoice);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Optional method to retrieve invoice by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }
    }

}
