namespace cart.services.invoice_service.Model
{
    public class InvoiceDTO
    {

        public long InvoiceId { get; set; }
        public DateTime? Date { get; set; }
        public string PhoneNumber { get; set; }
        public string Content { get; set; }
        public int? IsSucceed { get; set; }
        public int? TotalAmount { get; set; }

    }
}
