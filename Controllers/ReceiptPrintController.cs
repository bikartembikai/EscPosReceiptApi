using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ReceiptPrintController
    {
        [ApiController]
        [Route("api/[controller]")]
        public class PrintController : ControllerBase
        {
            private readonly ReceiptPrinterService _printerService;

            public PrintController(ReceiptPrinterService printerService)
            {
                _printerService = printerService;
            }

            [HttpPost("receipt")]
            public IActionResult PrintReceipt([FromBody] OrderDto order)
            {
                try
                {
                    _printerService.PrintReceipt(order);
                    return Ok(new { message = "Receipt sent to printer." });
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, new { error = "Printer error", details = ex.Message });
                }
            }
        }
    }
}
