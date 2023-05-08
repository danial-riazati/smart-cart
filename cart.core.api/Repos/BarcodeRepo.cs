using cart.core.api.Dtos;

namespace cart.core.api.Repos
{
    public class BarcodeRepo : IBarcodeRepo
    {
        public Queue<BarcodeDto> BarcodeQueue { get; } = new Queue<BarcodeDto>();
        public bool PostBarcode(BarcodeDto info)
        {
            BarcodeQueue.Enqueue(info);
            return true;
        }
        public BarcodeDto GetNextEvent()
        {
            return BarcodeQueue.Dequeue();
        }
    }
}
