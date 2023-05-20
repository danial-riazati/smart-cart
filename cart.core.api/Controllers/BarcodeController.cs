using cart.core.api.Dtos;
using cart.core.api.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;


namespace cart.core.api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BarcodeController : ControllerBase
    {
        private readonly IBarcodeRepo _repo;
        private readonly ILogger<BarcodeController> _logger;

        public BarcodeController(IBarcodeRepo repo, ILogger<BarcodeController> logger)
        {
            _repo = repo;
            _logger = logger;

        }
        [HttpPost]
        [Route("postBarcode")]
        public async Task<IActionResult> RecieveBarcode( BarcodeDto info)
        {
            Console.WriteLine($" barcode api recieved ,barcode is {info.barcode}");
            _logger.LogInformation("barcode api recieved");
            try
            {
                var res =  await _repo.PostBarcode(info);
                return Ok(res);

            }
            catch (Exception ex)
            {
                _logger.LogError($"exception in postBarcode:{ex.Message} ");
                return Problem(ex.Message + ex.StackTrace);
                

            }

        }


    }
}
