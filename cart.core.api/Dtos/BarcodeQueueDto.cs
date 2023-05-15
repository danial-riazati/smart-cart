namespace cart.core.api.Dtos
{
    public class BarcodeQueueDto
    {
        public string barcode { get; set; }
        public DateTime time { get; set; }
        public string itemId { get; set; }
    }
}
