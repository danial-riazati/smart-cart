using cart.core.api.Dtos;
using cart.core.api.Repos;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Microsoft.Extensions.Logging;

namespace cart.core.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeightController : ControllerBase
    {
        private readonly IWeightRepo _repo;
        private readonly ILogger<WeightController> _logger;


        public WeightController(IWeightRepo repo, ILogger<WeightController> logger)
        {
            _repo = repo;
            _logger = logger;

        }
        [HttpPost]
        [Route("postWeight")]
      
        public async Task<IActionResult> RecieveWeight(WeightDto info)
        {
          


            try
            {
                var res =  _repo.PostWeight(info);
                return Ok(res);

            }
            catch (Exception ex)
            {
               

                return Problem(ex.Message + ex.StackTrace);
            }

        }
        [HttpPost]
        [Route("WeightAndBarcode")]

        public async Task<IActionResult> WeightAndBarcode(WeightDto info)
        {
            _logger.LogInformation("weight api recieved");
            Console.WriteLine("recieve weight");
            try
            {
                var res = _repo.WeightAndBarcode(info);
                return Ok(res);

            }
            catch (Exception ex)
            {
                _logger.LogError($"exception in weight api : {ex.Message}");
                return Problem(ex.Message + ex.StackTrace);
            }

        }
    }
}
