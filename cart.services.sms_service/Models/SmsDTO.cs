using System;
namespace cart.services.sms_service.Models
{
    public class SmsDTO
    {
        public string phoneNumber { get; set; }
        public int code { get; set; }
        public int cartId { get; set; }
    }
}

