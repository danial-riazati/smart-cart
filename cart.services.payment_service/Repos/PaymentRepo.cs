using cart.services.payment_service.DataProvide;
using cart.services.payment_service.Models;
using Newtonsoft.Json.Linq;
using System.Text;

namespace cart.services.payment_service.Repos
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly PaymentDBContext _Paymentcontext;
        private readonly InvoiceDBContext _Invoicecontext;
        private readonly IConfiguration _configuration;

        string description = "سبد خرید";



        public PaymentRepo(PaymentDBContext paymentdBContext, InvoiceDBContext invoicedbContext, IConfiguration configuration)
        {
            _Paymentcontext = paymentdBContext;
            _Invoicecontext = invoicedbContext;
            _configuration = configuration;

        }
        public async Task<string?> GetPaymentUrl(int id)
        {
 
            try
            {
                var merchant = _configuration.GetValue<string>("merchant");
                var callBachUrl = _configuration.GetValue<string>("callBackUrl");
                var apiUrl = _configuration.GetValue<string>("PaymentApi");
                var amount = _Invoicecontext.Invoices.Where(x => x.InvoiceId == id).FirstOrDefault().TotalAmount;
                var peymentdto = new PeymentDTO(amount.ToString(), merchant, callBachUrl, description);                
                HttpClient _httpClient = new HttpClient();
                string json = System.Text.Json.JsonSerializer.Serialize(peymentdto);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, data);
                string responseBody = await response.Content.ReadAsStringAsync();

                JObject jodata = JObject.Parse(responseBody);
                
                var gatewayUrl = jodata["gatewayUrl"]?.ToString();
                if (amount == null)
                {
                    return null;
                }
                return gatewayUrl;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<string?> VerifyPayment(int id)
        {
            
            var verifyUrl = _configuration.GetValue<string>("VerifyApi");
            return null;

        }
    }
}
