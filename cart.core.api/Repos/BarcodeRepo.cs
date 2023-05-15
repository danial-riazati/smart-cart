using cart.core.api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace cart.core.api.Repos
{
    public class BarcodeRepo : IBarcodeRepo
    {
        private static Queue<BarcodeQueueDto> _barcodeQueue  = new Queue<BarcodeQueueDto>();
        public bool PostBarcode(BarcodeDto info)
        {
            BarcodeQueueDto barcodeQueueDto= new BarcodeQueueDto();
            barcodeQueueDto.time = DateTime.Now;
            barcodeQueueDto.barcode = info.barcode;
            _barcodeQueue.Enqueue(barcodeQueueDto);
            BarcodeQueueDto[] arr = _barcodeQueue.ToArray();
            return true;
        }
        public BarcodeQueueDto GetNextEvent()
        {
            return _barcodeQueue.Dequeue();
        }
        public Queue<BarcodeQueueDto> getQueue()
        {
            return _barcodeQueue;
        }
    }
}
