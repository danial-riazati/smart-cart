using cart.core.api.Dtos;
using cart.core.api.Repos;
using Microsoft.AspNetCore.Mvc;

namespace cart.core.api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BarcodeController : ControllerBase
    {
        private readonly IBarcodeRepo _repo;
        [HttpPost]
        [Route("postBarcode")]
        public async Task<IActionResult> RecieveBarcode(BarcodeDto info)
        {
            try
            {
                var res =  _repo.PostBarcode(info);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message + ex.StackTrace);
            }

        }


    }
}
