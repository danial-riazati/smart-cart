using cart.core.api.Repos;
using Microsoft.AspNetCore.Mvc;

namespace cart.core.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeightController : ControllerBase
    {
        private readonly IWeightRepo _repo;
        [HttpPost]
        [Route("postQRCode")]
        public async Task<IActionResult> RecieveWeight(string Weight)
        {
            try
            {
                var res = await _repo.RecieveWeightData(Weight);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message + ex.StackTrace);
            }

        }
    }
}
