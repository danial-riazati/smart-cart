using System.ComponentModel.DataAnnotations;

namespace cart.core.api.Dtos
{
    public class BarcodeDto
    {
        public TimestampAttribute timestamp { get; set; }
        public string barcode { get; set; }
    }
}
