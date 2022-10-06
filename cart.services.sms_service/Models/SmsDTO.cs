using System;
namespace cart.services.sms_service.Models
{
    public class SmsDTO
    {
        public string MobileNumber { get; set; }
        public int OtpCode { get; set; }
    }
}

