using System.Net;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using cart.services.product_service.Repos;
using Microsoft.AspNetCore.Mvc;

namespace cart.services.product_service.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        public ProductController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public IActionResult DownloadSqlightFile()
        {

            while (!_productRepo.CheckVersion())
            {
                _productRepo.GenerateProductSqlight();
            }
            var sqlversion = _productRepo.GetsqlightVersion();

            string filename = "Product" + sqlversion + ".sqlite";
            //string filePath = Path.Combine(_productRepo.GetVersionUrl(sqlversion), filename);

            string filePath = _productRepo.GetVersionUrl(sqlversion);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return File(fileStream, "application/octet-stream", filename);
        }
        [HttpGet]
        public string GetStoreDBVersion()
        {
            return _productRepo.GetStoreDbVersion();

        }
        [HttpPost]
        public async Task<IActionResult> UpdateSqlightFile()
        {
            _productRepo.GenerateProductSqlight();
            return Ok();

        }

    }
}
