using System.ComponentModel.DataAnnotations;

namespace cart.core.api.Dtos
{
    public class CameraDto
    {
        public string id { get; set; }
        public TimestampAttribute time { get; set; }
    }
}
