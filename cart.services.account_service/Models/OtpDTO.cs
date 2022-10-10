using System;
namespace cart.services.account_service.Models
{
    public class OtpDTO
    { 
            public string phoneNumber { get; set; }
        public int cartId { get; set; }
        public int code { get; set; }
    }
}

