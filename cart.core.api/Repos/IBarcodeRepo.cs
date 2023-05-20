using cart.core.api.Dtos;

namespace cart.core.api.Repos
{
    public interface IBarcodeRepo
    {
         Task<bool> PostBarcode(BarcodeDto info);
        BarcodeQueueDto GetNextEvent();
        Queue<BarcodeQueueDto> getQueue();
    }
}
