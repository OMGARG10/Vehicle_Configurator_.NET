using Microsoft.AspNetCore.Mvc;
using Vehicle_Configurator_Project.DTOS;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;
using Vehicle_Configurator_Project.Services;

namespace Vehicle_Configurator_Project.Controllers
{
    [ApiController]
    [Route("api/invoicedetail")]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly IInvoiceDetailService _invoiceDetailService;
        private readonly IAlternateComponentService _alternateComponentService;
        private readonly IHttpClientFactory _httpClientFactory;

        public InvoiceDetailController(
    IInvoiceDetailService invoiceDetailService,
    IAlternateComponentService alternateComponentService,
    IHttpClientFactory httpClientFactory)
        {
            _invoiceDetailService = invoiceDetailService;
            _alternateComponentService = alternateComponentService;
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public async Task<ActionResult<List<InvoiceDetail>>> GetAllInvoiceDetails()
        {
            var list = await _invoiceDetailService.GetAllInvoiceDetailsAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceDetail>> AddInvoiceDetail([FromBody] InvoiceDetail detail)
        {
            var created = await _invoiceDetailService.CreateInvoiceDetailAsync(detail);
            return CreatedAtAction(nameof(GetInvoiceDetailsByInvoice), new { invoiceId = created.InvoiceHeader.InvId }, created);
        }

        [HttpGet("byinvoice/{invoiceId}")]
public async Task<ActionResult<List<InvoiceDetail>>> GetInvoiceDetailsByInvoice(int invoiceId)
{
    try
    {
        var list = await _invoiceDetailService.GetInvoiceDetailsByInvoiceIdAsync(invoiceId);
        if (list == null || !list.Any())
            return NotFound();

        return Ok(list);
    }
    catch (Exception ex)
    {
        // Log to console or any logger you have
        Console.Error.WriteLine($"Error fetching invoice details for invoiceId {invoiceId}: {ex}");
        // Return detailed error to client (remove in production)
        return StatusCode(500, new { error = ex.Message, stack = ex.StackTrace });
    }
}



        [HttpPost("sendemail")]
        public async Task<ActionResult<string>> SendEmailViaMicroservice([FromBody] InvoiceWrapperDTO invoiceWrapperDto)
        {
            if (invoiceWrapperDto == null || invoiceWrapperDto.InvoiceDetails == null || !invoiceWrapperDto.InvoiceDetails.Any())
                return BadRequest("Invoice details are missing or invalid.");

            // Map DTO data to the payload for Java microservice
            var headerMap = new
            {
                invId = invoiceWrapperDto.InvoiceHeader?.InvId,
                custDetails = invoiceWrapperDto.InvoiceHeader?.CustDetails,
                invDate = invoiceWrapperDto.InvoiceHeader?.InvDate,  // string already
                quantity = invoiceWrapperDto.InvoiceHeader?.Quantity,
                finalAmount = invoiceWrapperDto.InvoiceHeader?.FinalAmount,
                tax = invoiceWrapperDto.InvoiceHeader?.Tax,
                totalAmount = invoiceWrapperDto.InvoiceHeader?.TotalAmount
            };

            var modelId = invoiceWrapperDto.ModelId;

            var detailsList = new List<object>();
            foreach (var detail in invoiceWrapperDto.InvoiceDetails)
            {
                if (detail?.Component == null) continue;

                decimal price = 0;

                if (string.Equals(detail.IsAlternate, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    // Assuming you have a method/service to get alternate price:
                    var alternates = await _alternateComponentService.GetByModelIdAndCompIdAsync(modelId, detail.Component.CompId ?? 0);
                    var match = alternates.FirstOrDefault();
                    if (match?.DeltaPrice != null)
                        price = match.DeltaPrice.Value;
                }
                // else price stays 0 or default

                detailsList.Add(new
                {
                    component = new
                    {
                        compId = detail.Component.CompId,
                        compName = detail.Component.CompName,
                        price = (double)price  // match Java's Double type
                    },
                    isAlternate = detail.IsAlternate
                });
            }

            var emailPayload = new
            {
                invoiceHeader = headerMap,
                invoiceDetails = detailsList
            };

            var client = _httpClientFactory.CreateClient();
            try
            {
                var response = await client.PostAsJsonAsync("http://localhost:9090/email/send", emailPayload);
                response.EnsureSuccessStatusCode();
                var respStr = await response.Content.ReadAsStringAsync();
                return Ok(respStr);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send email: {ex.Message}");
            }
        }

    }
}
