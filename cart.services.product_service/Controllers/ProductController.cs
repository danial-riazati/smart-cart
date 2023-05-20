using System.Net;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using cart.services.product_service.Repos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using cart.services.product_service.Models.DTOs;
using Microsoft.AspNetCore.Hosting.Server.Features;

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
        public async Task<IActionResult> DownloadSqliteFile()
        {
            var sqliteversion = _productRepo.GetLatestVersion();
            if (!_productRepo.CheckVersion(sqliteversion))
                sqliteversion =await _productRepo.UpdateProductSqlite(sqliteversion);
            string filename = "Product" + sqliteversion + ".sqlite";
            string filePath = _productRepo.GetVersionUrl(sqliteversion);

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, "application/octet-stream", filename);
        }

        [HttpGet]
        public IActionResult CheckVersion(string sqliteversion)
        {
            CheckVersionDTO dto = new();
            var connection = HttpContext.Connection;
            var downloadUrl = $"http://{connection.LocalIpAddress}:{connection.LocalPort}/api/Product/DownloadSqliteFile";
            dto.DownloadUrl = downloadUrl;

            if (!_productRepo.CheckVersion(sqliteversion))
            {
                _productRepo.UpdateProductSqlite(sqliteversion);
                dto.Message = "Your db is not up to date. ";
            }
            else dto.Message = "Your db is up to date. ";

            return Ok(dto);
        }
    

    }
}
