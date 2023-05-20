namespace cart.services.payment_service.Models
{
    public class PeymentDTO
    {
        public string amount { set; get; }
        public string merchant { set; get; }
        public string callBackUrl { set; get; }
        public string description { set; get; }

        public PeymentDTO(string amount, string merchant, string callBackUrl, string description)
        {
            this.amount = amount;
            this.merchant = merchant;
            this.callBackUrl = callBackUrl;
            this.description = description;
        }
    }

}
