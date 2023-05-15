using cart.core.api.Dtos;
using cart.core.api.Repos;
using Microsoft.AspNetCore.Mvc;

namespace cart.core.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeightController : ControllerBase
    {
        private readonly IWeightRepo _repo;

        public WeightController(IWeightRepo repo)
        {
            _repo = repo;

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
            try
            {
                var res = _repo.WeightAndBarcode(info);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message + ex.StackTrace);
            }

        }
    }
}
