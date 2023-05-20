using cart.services.payment_service.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace cart.services.payment_service.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepo _paymentRepo;
        public PaymentController(IPaymentRepo productRepo)
        {
            _paymentRepo = productRepo;
        }


        [HttpGet]
        public IActionResult GetHtmlFile()
        {

            string ErrorfilePath = Path.Combine(Environment.CurrentDirectory , "\\Views\\error.html");
            string successfilePath = Path.Combine(Environment.CurrentDirectory , "\\Views\\successful.html");


            //if (!System.IO.File.Exists(filePath))
            //{
            //    return NotFound();
            //}

             string fileContent = System.IO.File.ReadAllText(ErrorfilePath, Encoding.UTF8);
            return Content(fileContent, "text/html; charset=utf-8");


        }
        [HttpGet]
        public IActionResult VerifyPeyment()
        {
            string queryString = HttpContext.Request.QueryString.Value;
            NameValueCollection queryParameters = HttpUtility.ParseQueryString(queryString);
            //var verify = await _paymentRepo.VerifyPayment();
            return null;


        }


    }

}
