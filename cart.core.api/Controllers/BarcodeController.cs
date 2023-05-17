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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public BarcodeController(IBarcodeRepo repo)
        {
            _repo = repo;

        }
        [HttpPost]
        [Route("postBarcode")]
        public async Task<IActionResult> RecieveBarcode( BarcodeDto info)
        {
            Console.WriteLine(" barcode api recieved");
            _logger.Info("barcode api recieved");
            try
            {
                var res =  _repo.PostBarcode(info);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message + ex.StackTrace);
                _logger.Info($"exception in postBarcode:{ex.Message} ");

            }

        }


    }
}
